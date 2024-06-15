import { Component, Inject, NgModule } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms'; // Importăm FormsModule pentru ngModel
import { ReactiveFormsModule } from '@angular/forms'; // Importăm ReactiveFormsModule pentru FormBuilder
import { CardService } from '../../services/card.service';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';




@Component({
  imports: [
    HttpClientModule,

    CommonModule,
    FormsModule, // Adăugăm FormsModule aici pentru ngModel
    ReactiveFormsModule, // Adăugăm ReactiveFormsModule aici pentru FormBuilder
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    
  ],
  standalone: true,



  selector: 'app-date-picker-dialog',
  templateUrl: './date-picker-dialog.component.html'
})
export class DatePickerDialogComponent {
  startDate: Date = new Date();
  endDate: Date  = new Date();

  constructor(public dialogRef: MatDialogRef<DatePickerDialogComponent>) {}

  onNoClick(): void {
    this.dialogRef.close();
  }

  onSelectDates(): void {
    if (this.startDate && this.endDate) {
      this.dialogRef.close({ startDate: this.startDate, endDate: this.endDate });
    }
  }
}
