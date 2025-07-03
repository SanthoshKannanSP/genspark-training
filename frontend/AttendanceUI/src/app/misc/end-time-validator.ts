import { AbstractControl, ValidationErrors } from '@angular/forms';

export function endAfterStartValidator(
  group: AbstractControl
): ValidationErrors | null {
  const startTime = group.get('startTime')?.value;
  const endTime = group.get('endTime')?.value;
  if (!startTime || !endTime) return null;

  if (endTime <= startTime) return { endBeforeStart: true };
  return null;
}
