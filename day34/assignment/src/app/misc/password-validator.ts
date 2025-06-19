import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function passwordValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value: string = control.value;
    if (value == null) return { emptyError: 'Empty' };
    let hasUpperCase = false;
    let hasLowerCase = false;
    let hasDigit = false;
    const allowedSymbols = '!@#$%&*';
    let hasSymbol = false;

    for (let char of value) {
      if (char >= 'A' && char <= 'Z') hasUpperCase = true;
      else if (char >= 'a' && char <= 'z') hasLowerCase = true;
      else if (char >= '0' && char <= '9') hasDigit = true;
      else if (allowedSymbols.includes(char)) hasSymbol = true;
      else return { passwordError: 'Password contains unpermitted symbols' };
    }

    if (!hasUpperCase)
      return {
        passwordError: 'Password should contain atleast one uppercase letter',
      };
    if (!hasLowerCase)
      return {
        passwordError: 'Password should contain atleast one lowercase letter',
      };
    if (!hasDigit)
      return {
        passwordError: 'Password should contain atleast one digit',
      };
    if (!hasSymbol)
      return {
        passwordError: 'Password should contain atleast one permitted symbol',
      };

    return null;
  };
}
