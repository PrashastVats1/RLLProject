import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfirmPasswordValidator } from 'src/app/helpers/confirm-password.validator';
import ValidateForm from 'src/app/helpers/validateform';
import { ResetPassword } from 'src/app/models/reset-password.model';
import { ResetPasswordService } from 'src/app/services/reset-password.service';
@Component({
  selector: 'app-reset',
  templateUrl: './reset.component.html',
  styleUrls: ['./reset.component.css'],
})
export class ResetComponent implements OnInit {
  resetPasswordForm!: FormGroup;
  emailToReset!: string;
  emailToken!: string;
  resetPasswordObj = new ResetPassword();
  // For New Password
  isNewPasswordText: boolean = false;
  newPasswordEyeIcon: string = 'fa-eye-slash';

  // For Confirm Password
  isConfirmPasswordText: boolean = false;
  confirmPasswordEyeIcon: string = 'fa-eye-slash';

  constructor(
    private fb: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private resetService: ResetPasswordService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.resetPasswordForm = this.fb.group(
      {
        password: [null, Validators.required],
        confirmPassword: [null, Validators.required],
      },
      {
        validator: ConfirmPasswordValidator('password', 'confirmPassword'),
      }
    );

    this.activatedRoute.queryParams.subscribe((val) => {
      this.emailToReset = val['email'];
      let uriToken = val['code'];
      this.emailToken = uriToken.replace(/ /g, '+');
      console.log(this.emailToReset);
      console.log(this.emailToken);
    });
  }
  hideShowNewPassword() {
    this.isNewPasswordText = !this.isNewPasswordText;
    this.isNewPasswordText
      ? (this.newPasswordEyeIcon = 'fa-eye')
      : (this.newPasswordEyeIcon = 'fa-eye-slash');
  }

  hideShowConfirmPassword() {
    this.isConfirmPasswordText = !this.isConfirmPasswordText;
    this.isConfirmPasswordText
      ? (this.confirmPasswordEyeIcon = 'fa-eye')
      : (this.confirmPasswordEyeIcon = 'fa-eye-slash');
  }

  reset() {
    if (this.resetPasswordForm.valid) {
      this.resetPasswordObj.email = this.emailToReset;
      this.resetPasswordObj.newPassword = this.resetPasswordForm.value.password;
      this.resetPasswordObj.confirmPassword =
        this.resetPasswordForm.value.confirmPassword;
      this.resetPasswordObj.emailToken = this.emailToken;

      this.resetService.resetPassword(this.resetPasswordObj).subscribe({
        next: (res) => {
          this.toastr.success('Password Reset Successfully', 'Success');
          this.router.navigate(['login']);
        },
        error: (err) => {
          this.toastr.error('Something went wrong', 'Error');
          console.log(err);
        },
      });
    } else {
      ValidateForm.validateAllFormFields(this.resetPasswordForm);
    }
  }
}
