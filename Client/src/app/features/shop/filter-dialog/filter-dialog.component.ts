import { Component, inject } from '@angular/core';
import { ShopserviceService } from '../../../Core/Services/shopservice.service';
import {MatDivider} from '@angular/material/divider';
import {MatListOption, MatSelectionList} from '@angular/material/list';
import { MatButton } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
import { MatIcon } from '@angular/material/icon';
@Component({
  selector: 'app-filter-dialog',
  standalone: true,
  imports: [
    MatDivider,
     MatSelectionList,
     MatListOption,
     MatButton,
     FormsModule,
     MatIcon
  ],
  templateUrl: './filter-dialog.component.html',
  styleUrl: './filter-dialog.component.scss'
})
export class FilterDialogComponent {
shopeservice = inject(ShopserviceService)
private dialogRef = inject(MatDialogRef<FilterDialogComponent>);
data = inject(MAT_DIALOG_DATA);
selectedBrands : string[] = this.data.selectedBrands;
selectedTypes : string[] = this.data.selectedTypes;

applyFilters(){
  this.dialogRef.close({
    selectedBrands : this.selectedBrands,
    selectedTypes: this.selectedTypes
  })
}
clearFilters(){
  this.selectedBrands = [];
  this.selectedTypes = [];
}
}
