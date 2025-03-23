export type CartType = {
 id?:number;
 items:Items[];
}

export type Items={
   productId:number;
   productName:string;
   price:number;
   quantity:number;
   pictureUrl:string;
   brand:string;
   type:string
}
export class Cart implements CartType {
   id?: number;  // Make the id optional
   items: Items[] = [];
 
   constructor() {
     const storedId = localStorage.getItem('cart_id');
     if (storedId) {
       this.id = parseInt(storedId, 10);  // If an id exists, use it
     }
     // Otherwise, let the backend handle the id generation
   }
 
 
 }
 