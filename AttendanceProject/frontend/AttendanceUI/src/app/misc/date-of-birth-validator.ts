import { AbstractControl, ValidationErrors } from '@angular/forms';

export function dateOfBirthValidator(
  control: AbstractControl
): ValidationErrors | null {
  const dobValue = new Date(control.value);
  const today = new Date();

  if (isNaN(dobValue.getTime())) {
    return { invalidDate: true };
  }

  const ageDiff = today.getFullYear() - dobValue.getFullYear();
  const isPastDate = dobValue < today;
  const isOldEnough = ageDiff > 12;

  if (!isPastDate) {
    return { notInPast: true };
  }

  if (!isOldEnough) {
    return { tooYoung: true };
  }

  return null;
}
