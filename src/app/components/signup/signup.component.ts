import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import ValidateForm from 'src/app/helpers/validateform';
import { AuthService } from 'src/app/services/auth.service';
import { ToastrService } from 'ngx-toastr'; // <-- Import ToastrService

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
})
export class SignupComponent implements OnInit {
  type: string = 'password';
  isText: boolean = false;
  eyeIcon: string = 'fa-eye-slash';
  signupForm!: FormGroup;
  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private router: Router,
    private toastr: ToastrService // <-- Inject ToastrService
  ) {}

  ngOnInit(): void {
    this.signupForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      userName: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  hideShowPass() {
    this.isText = !this.isText;
    this.isText ? (this.eyeIcon = 'fa-eye') : (this.eyeIcon = 'fa-eye-slash');
    this.isText ? (this.type = 'text') : (this.type = 'password');
  }

  onSignup() {
    if (this.signupForm.valid) {
      this.auth.signUp(this.signupForm.value).subscribe({
        next: (res) => {
          this.toastr.success(res.message, 'Success'); // <-- Using toastr for success message
          this.signupForm.reset();
          this.router.navigate(['login']);
        },
        error: (err) => {
          console.log(err);
          this.toastr.error(
            err?.error?.message || 'An error occurred',
            'Error'
          ); // <-- Using toastr for error message
        },
      });
    } else {
      ValidateForm.validateAllFormFields(this.signupForm);
      this.toastr.error('Invalid Form', 'Error'); // <-- Notify invalid form using toastr
    }
  }
}
