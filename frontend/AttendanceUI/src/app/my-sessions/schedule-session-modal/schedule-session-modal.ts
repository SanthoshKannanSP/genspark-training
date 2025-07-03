import { Component, ElementRef, inject, ViewChild } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ScheduleSessionModel } from '../../models/schedule-session-model';
import { SessionService } from '../../services/session-service';
import { NotificationService } from '../../services/notification-service';
import { futureDateValidator } from '../../misc/future-date-validator';
import { endAfterStartValidator } from '../../misc/end-time-validator';

@Component({
  selector: 'app-schedule-session-button',
  imports: [ReactiveFormsModule],
  templateUrl: './schedule-session-modal.html',
  styleUrl: './schedule-session-modal.css',
})
export class ScheduleSessionModal {
  sessionService = inject(SessionService);
  notificationService = inject(NotificationService);
  sessionForm: FormGroup;
  @ViewChild('closeButton') closeButton!: ElementRef<HTMLButtonElement>;

  constructor(private fb: FormBuilder) {
    this.sessionForm = this.fb.group(
      {
        sessionName: ['', [Validators.required, Validators.minLength(1)]],
        date: ['', [Validators.required, futureDateValidator]],
        startTime: ['', Validators.required],
        endTime: ['', Validators.required],
        sessionLink: ['', Validators.required],
      },
      {
        validators: [endAfterStartValidator],
      }
    );
  }

  onSubmit() {
    this.sessionService
      .scheduleSession(this.sessionForm.value as ScheduleSessionModel)
      .subscribe({
        next: (data: any) => {
          this.sessionForm.reset();
          this.closeButton.nativeElement.click();
          const message =
            data.data.sessionName + ' has been successfully scheduled';
          this.notificationService.addNotification({
            message: message,
            type: 'success',
          });
        },
        error: (err) => {
          console.log(err);
          this.notificationService.addNotification({
            message: 'Unable to create session',
            type: 'danger',
          });
        },
      });
  }
}
