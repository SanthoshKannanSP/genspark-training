import {
  AbstractControl,
  ValidationErrors,
  FormGroup,
  ValidatorFn,
} from '@angular/forms';

export function confirmPasswordValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const formGroup = control as FormGroup;
    const password = formGroup.get('password')?.value;
    const confirmPassword = formGroup.get('confirmPassword')?.value;

    if (password != confirmPassword) {
      return { passwordMismatch: 'Passwords mismatch' };
    }

    return null;
  };
}
