import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import ValidateForm from 'src/app/helpers/validateform';
import { AuthService } from 'src/app/services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { UserStoreService } from 'src/app/services/user-store.service';
import { ResetPasswordService } from 'src/app/services/reset-password.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  public loginForm!: FormGroup;
  type: string = 'password';
  isText: boolean = false;
  eyeIcon: string = 'fa-eye-slash';
  public resetPasswordEmail!: string;
  public isValidEmail!: boolean;
  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private router: Router,
    private toastr: ToastrService,
    private userStore: UserStoreService,
    private resetService: ResetPasswordService
  ) {}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }
  hideShowPass() {
    this.isText = !this.isText;
    this.isText ? (this.eyeIcon = 'fa-eye') : (this.eyeIcon = 'fa-eye-slash');
    this.isText ? (this.type = 'text') : (this.type = 'password');
  }
  onLogin() {
    if (this.loginForm.valid) {
      console.log(this.loginForm.value);
      //send object to database
      this.auth.login(this.loginForm.value).subscribe({
        next: (res) => {
          console.log(res.message);
          this.loginForm.reset();
          this.auth.storeToken(res.token);
          const tokenPayload = this.auth.decodedToken();
          this.userStore.setFullNameForStore(tokenPayload.name);
          this.userStore.setRoleForStore(tokenPayload.role);
          //alert(res.message);
          // Using toastr for success message
          this.toastr.success(res.message, 'Success');
          this.router.navigate(['dashboard']);
        },
        error: (err) => {
          //alert(err?.error.message);
          // Using toastr for error message
          this.toastr.error('UserName or Password was incorrect', 'Error');
          console.log(err);
        },
      });
    } else {
      //console.log('Form is not valid');
      //throw an error using toaster and with required field
      ValidateForm.validateAllFormFields(this.loginForm);
      // Using toastr to notify invalid form
      this.toastr.error('Your form is invalid', 'Error');
      //alert('Your form is invalid');
    }
  }

  checkValidEmail(event: string) {
    const value = event;
    const pattern = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
    this.isValidEmail = pattern.test(value);
    return this.isValidEmail;
  }

  confirmToSend() {
    if (this.checkValidEmail(this.resetPasswordEmail)) {
      console.log(this.resetPasswordEmail);
      // this.resetPasswordEmail = '';
      // const buttonRef = document.getElementById('closeBtn');
      // buttonRef?.click();
      //api call
      this.resetService
        .sendResetPasswordLink(this.resetPasswordEmail)
        .subscribe({
          next: (res) => {
            this.toastr.success('Success', 'Reset Link Sent Successfully');
            this.resetPasswordEmail = '';
            const buttonRef = document.getElementById('closeBtn');
            buttonRef?.click();
          },
          error: (err) => {
            this.toastr.error('Something went wrong', 'ERROR');
          },
        });
    }
  }
  resolved(captchaResponse: string) {
    console.log(`Resolved CAPTCHA with response: ${captchaResponse}`);
    // You can send this response to your backend to verify it using the secret key.
  }
}
