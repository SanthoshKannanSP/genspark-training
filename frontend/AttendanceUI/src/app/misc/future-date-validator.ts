import { AbstractControl, ValidationErrors } from '@angular/forms';

export function futureDateValidator(
  control: AbstractControl
): ValidationErrors | null {
  const inputDate = new Date(control.value);
  const tomorrow = new Date();
  tomorrow.setDate(tomorrow.getDate() + 1);
  inputDate.setHours(0, 0, 0, 0);
  tomorrow.setHours(0, 0, 0, 0);

  if (inputDate <= tomorrow) return { dateNotInFuture: true };

  return null;
}
