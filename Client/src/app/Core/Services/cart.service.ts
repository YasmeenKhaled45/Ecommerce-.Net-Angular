import { computed, inject, Injectable, signal } from '@angular/core'; 
import { environment } from '../../../environments/environment'; 
import { HttpClient } from '@angular/common/http'; 
import { Cart, Items } from '../../Shared/Models/Cart'; 
import { product } from '../../Shared/Models/product'; 
import { map } from 'rxjs'; 

@Injectable({ 
  providedIn: 'root' 
}) 
export class CartService { 
  baseUrl = 'https://localhost:5001/api/cart/'; 
  private http = inject(HttpClient); 
  cart = signal<Cart | null>(null); 

  itemCount = computed(() => { 
    const cart = this.cart(); 
    console.log('Calculating item count. Current cart:', cart); 
    return cart?.items?.reduce((sum, item) => sum + item.quantity, 0) || 0; 
  }); 

  totals = computed(()=>{
    const cart = this.cart();
    if(!cart) return null;
    const subtotal = cart.items.reduce((sum, item) => sum + item.price * item.quantity, 0);
    const shipping = 0;
    const discount =0;
    return{
      subtotal,
      shipping,
      discount,
      total: subtotal+ shipping - discount
    }
  })

  constructor() {
    // Load cart from local storage on initialization
    const savedCart = localStorage.getItem('cart'); 
    if (savedCart) { 
      const cart = JSON.parse(savedCart); 
      this.cart.set({ 
        ...cart, 
        items: cart.items ?? [], 
      }); 
      console.log('Loaded cart from localStorage:', this.cart()); 
    }
  }

  getCart(id: string) { 
    let cartId = localStorage.getItem('cart_id'); 
    cartId = cartId && !isNaN(Number(cartId)) ? cartId : '0';
    // Fetch from backend if no cart in localStorage
    return this.http.get<Cart>(`${this.baseUrl}?id=${cartId || 0}`).pipe( 
      map(cart => { 
        if (cart?.id) { 
          localStorage.setItem('cart_id', cart.id.toString()); 
        } 
        this.cart.set({ 
          ...cart, 
          items: cart.items ?? [], 
        }); 
        console.log('Cart after fetching:', this.cart()); 
        return this.cart(); 
      }) 
    ); 
  }  

  // Save the updated cart to the backend
  setCart(cart: Cart) { 
    const payload = { ...cart, items: cart.items }; 

    this.http.post<Cart>(this.baseUrl + 'setcart', payload).subscribe({ 
      next: updatedCart => { 
        if (updatedCart?.id) { 
          localStorage.setItem('cart_id', updatedCart.id.toString()); 
        } 

        // Save the updated cart to localStorage
        localStorage.setItem('cart', JSON.stringify(updatedCart)); 

        const items = updatedCart.items ?? [];  
        this.cart.set({ 
          ...updatedCart, 
          items: items,  
        }); 

        console.log('Cart after saving:', this.cart()); 
        console.log('Item Count:', this.itemCount()); 
      }, 
      error: err => { 
        console.error('Error setting cart:', err); 
        if (err.error) { 
          console.error('Error details:', err.error); 
        } 
      }     
    }); 
  } 

  // Add item to the cart
  addItemToCart(item: Items | product, quantity = 1) { 
    console.log('Adding item to cart:', item, quantity); 
    const cart = this.cart() || this.createCart(); // Ensure we have a cart
    console.log('Current cart before adding item:', cart); 

    if (this.isProduct(item)) { 
      item = this.mapProductToItem(item, quantity); 
    } 
    
    // Update the items in the cart
    cart.items = this.addOrUpdateItem(cart.items, item as Items, quantity); 

    console.log('Updated cart before saving:', cart); 
    this.setCart(cart); // This will handle the mapping and saving process
  } 

  private addOrUpdateItem(items: Items[], item: Items, quantity: number): Items[] { 
    const index = items.findIndex(x => x.productId === item.productId); 
    
    if (index === -1) { // Item not found in the cart
      item.quantity = quantity;
      return [...items, item]; // Add new item
    } else {  const updatedItems = [...items];
      updatedItems[index] = { ...updatedItems[index], quantity: updatedItems[index].quantity + quantity }; // Update quantity
      return updatedItems;
    } 
  } 
 
  removeItemfromCart(id:number,quantity=1){
   const cart = this.cart();
   if(!cart) return ;
   const index = cart.items.findIndex(x=>x.productId === id);
   if(index !== -1){
    if(cart.items[index].quantity > quantity){
      cart.items[index].quantity -= quantity;
    } else{
      cart.items.splice(index,1);
    }
    if(cart.items.length == 0){
      this.deleteCart()
    } else{
      this.setCart(cart);
    }
   }else {
    console.warn('Product ID ${productId} not found in cart.');
  }
}
deleteCart() {
  const cartId = this.cart()?.id; // Get the current cart ID

  if (!cartId) {
    console.error('No cart ID available to delete');
    return; // Exit if there's no cart ID
  }

  // Ensure the correct URL format when calling the backend
  const url = `${this.baseUrl}${cartId}`; // Assuming your URL follows the pattern /api/cart/{id}

  this.http.delete(url).subscribe({
    next: () => {
      localStorage.removeItem('cart_id'); // Remove the cart ID from localStorage
      localStorage.removeItem('cart');
      this.cart.set(null); // Clear the cart state
      console.log('Cart deleted successfully');
    },
    error: (err) => {
      console.error('Error deleting cart:', err); // Log the full error for debugging
    },
  });
}

  private mapProductToItem(item: product, quantity: number): Items { 
    return { 
      productId: item.id, 
      productName: item.name, 
      pictureUrl: item.pictureUrl, 
      price: item.price, 
      quantity: quantity, // Set the provided quantity
      type: item.type, 
      brand: item.brand, 
    }; 
  } 

  private isProduct(item: Items | product): item is product { 
    return (item as product).id !== undefined; // Type guard to determine if it's a product
  }

  private createCart(): Cart {
    return { items: [] }; // Initialize an empty cart if none exists
  }
}
