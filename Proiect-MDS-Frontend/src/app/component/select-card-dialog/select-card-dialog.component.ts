import { Component, Inject, NgModule } from '@angular/core';
import {
  MatDialogRef,
  MAT_DIALOG_DATA,
  MatDialogModule,
} from '@angular/material/dialog';
import { FormsModule } from '@angular/forms'; // Importăm FormsModule pentru ngModel
import { ReactiveFormsModule } from '@angular/forms'; // Importăm ReactiveFormsModule pentru FormBuilder
import { CardService } from '../../services/card.service';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { HttpClient } from '@angular/common/http';

@Component({
  imports: [
    HttpClientModule,

    CommonModule,
    FormsModule, // Adăugăm FormsModule aici pentru ngModel
    ReactiveFormsModule, // Adăugăm ReactiveFormsModule aici pentru FormBuilder
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
  ],
  standalone: true,
  providers: [CardService],
  selector: 'app-select-card-dialog',
  templateUrl: './select-card-dialog.component.html',
})
export class SelectCardDialogComponent {
  cards: any[] = [];
  newCard: any = { name: '', details: '' }; // Variabila pentru noul card

  constructor(
    private cardService: CardService,
    public dialogRef: MatDialogRef<SelectCardDialogComponent>,
    private http: HttpClient,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.loadCards();
  }

  loadCards() {
    this.cardService.getCards().subscribe((cards) => {
      this.cards = cards;
    });
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onAddCard(): void {
    if (this.isFormValid()) {
      // Aici ar trebui să adăugăm noul card
      this.dialogRef.close(this.newCard); // Închidem dialogul și trimitem noul card
    }
  }

  onSelectCard(card: any): void {
    this.dialogRef.close(card);
  }

  isFormValid(): boolean {
    return (
      this.newCard.name.trim() !== '' && this.newCard.details.trim() !== ''
    );
  }
}
