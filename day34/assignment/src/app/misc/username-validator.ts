import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function usernameValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value: string = control.value;
    const bannedWords = ['root', 'admin', 'superuser'];
    let containsBannedWords = false;
    bannedWords.forEach((word) => {
      if (value?.includes(word)) containsBannedWords = true;
    });

    if (containsBannedWords)
      return {
        bannedWordsError:
          'Username contains banned keywords. Please try another username',
      };
    return null;
  };
}
