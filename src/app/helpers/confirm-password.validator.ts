import { FormGroup } from '@angular/forms';

export function ConfirmPasswordValidator(
  controlName: string,
  matchControlName: string
) {
  return (formGroup: FormGroup) => {
    const passwordControl = formGroup.controls[controlName];
    const confirmPasswordControl = formGroup.controls[matchControlName];

    if (!confirmPasswordControl || !passwordControl) {
      return null;
    }

    // if field is empty and required error is already set
    if (
      !confirmPasswordControl.value &&
      confirmPasswordControl.errors &&
      confirmPasswordControl.errors['required']
    ) {
      return null;
    }

    if (passwordControl.value !== confirmPasswordControl.value) {
      confirmPasswordControl.setErrors({
        ...confirmPasswordControl.errors,
        confirmPasswordValidator: true,
      });
    } else {
      const { confirmPasswordValidator, ...otherErrors } =
        confirmPasswordControl.errors || {};
      confirmPasswordControl.setErrors(
        Object.keys(otherErrors).length ? otherErrors : null
      );
    }

    return null;
  };
}
