import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { RouterOutlet, Router, ActivatedRoute } from '@angular/router';
import { UserService } from '../../services/user.service';
import { validatorParola } from '../../validators/user.validator';

@Component({
  selector: 'app-password-reset',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
  ],
  providers: [UserService],
  templateUrl: './password-reset.component.html',
  styleUrl: './password-reset.component.css',
})
export class PasswordResetComponent implements OnInit {
  newPassword: string = '';
  confirmPassword: string = '';
  username: string = '';
  token: string = '';
  errorMessage: string = '';

  resetForm: FormGroup;
  constructor(
    public router: Router,
    private fb: FormBuilder,
    private params: ActivatedRoute,
    private userService: UserService
  ) {
    this.params.queryParams.subscribe((k) => {
      this.username = k['username'];
      this.token = k['token'];
    });

    this.resetForm = this.fb.group({
      username: [this.username],
      token: [this.token],
      newPassword: ['', [Validators.required, validatorParola]],
      confirmPassword: ['', [Validators.required, validatorParola]],
    });
  }
  resetPassword() {
    if (this.resetForm.invalid) {
      this.markFormGroupTouched(this.resetForm);
      return;
    }

    if (this.newPassword !== this.confirmPassword) {
      this.errorMessage = 'Passwords do not match!';
      return;
    }

    const resetData = {
      username: this.username,
      token: this.token,
      newPassword: this.newPassword,
    };
    this.resetForm.value.password = this.newPassword;
    console.log(this.resetForm.value);
    this.userService.resetPassword(this.resetForm.value).subscribe(
      (data) => {
        console.log(data);
        this.router.navigate(['/login']);
      },
      (error) => {
        console.log(error);
        this.errorMessage = error.error;
      }
    );
  }
  ngOnInit() {
    this.resetForm = this.fb.group({
      username: [this.username],
      token: [this.token],
      newPassword: [null],
      confirmPassword: [null],
    });
  }
  private markFormGroupTouched(formGroup: FormGroup) {
    Object.values(formGroup.controls).forEach((control) => {
      control.markAsTouched();
      if ((control as any).controls) {
        this.markFormGroupTouched(control as FormGroup);
      }
    });
  }
}
