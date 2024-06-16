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
import { UserService } from '../../services/user.service';
import { CookieService } from 'ngx-cookie-service';
import { jwtDecode } from 'jwt-decode';
@Component({
  imports: [
    HttpClientModule,
    CommonModule,
    FormsModule, 
    ReactiveFormsModule, 
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
  ],
  standalone: true,
  providers: [CardService,UserService,NgModule],
  selector: 'app-select-card-dialog',
  templateUrl: './select-card-dialog.component.html',
})

export class SelectCardDialogComponent {
  cards: any[] = [];
  newCard: any = {userId:0, nume: '', numar: '',dataExpirare:null,cvv:0 }; // Variabila pentru noul card
  token=this.cookieService.get('token');
  userid=jwtDecode(this.token);
  user: any;
  constructor(
    private cardService: CardService,
    public dialogRef: MatDialogRef<SelectCardDialogComponent>,
    private http: HttpClient,
    private cookieService: CookieService,
    private userService:UserService,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    const decodedToken: { [key: string]: any } = jwtDecode(this.token);
    this.user = decodedToken['id'];
    this.newCard.userId=this.user;
    this.loadCards();
  }
  selectCard(card:any){
    this.newCard.nume=card.nume;
    this.newCard.numar=card.numar;
    this.newCard.dataExpirare=card.dataExpirare;
    this.newCard.cvv=card.cvv;
    this.dialogRef.close(this.newCard);
  }
  loadCards() {
    this.cardService.getCards(this.user).subscribe((cards) => {
      this.cards = cards;
    });
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onAddCard(): void {
    if (this.isFormValid()) {
      this.newCard.dataExpirare=this.convertMMYYToDate(this.newCard.dataExpirare);
      this.cardService.addCard(this.newCard).subscribe(() => {
      this.dialogRef.close(this.newCard);},(error)=>{console.log(error)
        console.log(this.newCard);
      }
      );
    }
    else {
      console.log("Invalid form"+this.newCard.nume+" "+this.newCard.numar+" "+this.newCard.dataExpirare+" "+this.newCard.cvv)
      console.log(this.isValidExpiryDate(this.newCard.dataExpirare))
      console.log(this.newCard.cvv > 99 && this.newCard.cvv < 1000)
      console.log(this.newCard.numar.trim().length)
      console.log(this.newCard.nume.trim() !== '')
    }
  }
  isValidExpiryDate(dateStr: string): boolean {
    const regex = /^(0[1-9]|1[0-2])\/\d{2}$/;
    if (!regex.test(dateStr)) {
        return false;
    }
    const [lunaStr, anStr] = dateStr.split('/');
    const luna = parseInt(lunaStr, 10);
    const an = parseInt(anStr, 10);
    const now = new Date();
    const lunaCurenta = now.getMonth() + 1; 
    const anCurent = now.getFullYear() % 100;
    if (an > anCurent || (an === anCurent && luna > lunaCurenta)) {
        return true;
    }
    return false;
}
 convertMMYYToDate(mmyy:string) {
  let [month, year] = mmyy.split('/');
  const date = new Date(parseInt(year, 10)+2000, parseInt(month, 10) - 1);
  return date;
}
  onSelectCard(card: any): void {
    this.dialogRef.close(card);
  }

  isFormValid(): boolean {
    return (
      this.newCard.nume.trim() !== '' && this.newCard.numar.trim().length==16 && this.isValidExpiryDate(this.newCard.dataExpirare) && this.newCard.cvv > 99 && this.newCard.cvv < 1000
    );
  }
}
