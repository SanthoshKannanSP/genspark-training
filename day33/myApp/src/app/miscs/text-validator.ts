import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function textValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    if (value?.length < 6)
      return { lenError: 'Password should have minimum 6 letters' };
    return null;
  };
}
