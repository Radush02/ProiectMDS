import { Component, Inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import {
  MatDialogRef,
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogModule,
} from '@angular/material/dialog';

@Component({
  selector: 'app-message-pop-up',
  standalone: true,
  imports: [MatButtonModule, MatDialogModule],
  templateUrl: './message-pop-up.component.html',
  styleUrl: './message-pop-up.component.css',
})
export class MessagePopUpComponent {
  constructor(
    public dialogRef: MatDialogRef<MessagePopUpComponent>,
    @Inject(MAT_DIALOG_DATA) public data: string
  ) {}

  closeDialog() {
    this.dialogRef.close();
  }
}
