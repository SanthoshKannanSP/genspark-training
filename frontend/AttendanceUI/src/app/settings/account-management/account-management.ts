import { Component, inject, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AccountService } from '../../services/account-service';
import { StudentModel } from '../../models/student-model';
import { TeacherModel } from '../../models/teacher-model';
import { HttpClientService } from '../../services/http-client-service';

@Component({
  selector: 'app-account-management',
  imports: [ReactiveFormsModule, FormsModule],
  templateUrl: './account-management.html',
  styleUrl: './account-management.css',
})
export class AccountManagement {
  accountForm!: FormGroup;
  accountService = inject(AccountService);
  api = inject(HttpClientService);

  constructor(private fb: FormBuilder) {
    if (this.api.hasRole('Student')) {
      this.accountForm = this.fb.group({
        name: ['', [Validators.required, Validators.minLength(1)]],
        dateOfBirth: ['', [Validators.required, this.pastDateValidator]],
        gender: ['', [Validators.required]],
      });
    } else if (this.api.hasRole('Teacher')) {
      this.accountForm = this.fb.group({
        name: ['', [Validators.required, Validators.minLength(1)]],
        organization: ['', Validators.required],
      });
    }

    this.accountService.account$.subscribe({
      next: (data) => {
        this.accountForm.patchValue(data);
      },
    });
  }

  pastDateValidator(control: any) {
    const date = new Date(control.value);
    const today = new Date();
    const minAge = new Date(
      today.getFullYear() - 12,
      today.getMonth(),
      today.getDate()
    );
    return date < minAge ? null : { tooYoung: true };
  }

  onSubmit() {
    if (this.accountForm.valid) {
      this.accountService.updateAccountDetails(this.accountForm.value);
    }
  }

  onDeleteAccount() {
    if (
      confirm(
        'Are you sure you want to delete your account? This action is irreversible.'
      )
    ) {
      console.log('Deleting account...');
      this.accountService.deleteAccount();
    }
  }
}
