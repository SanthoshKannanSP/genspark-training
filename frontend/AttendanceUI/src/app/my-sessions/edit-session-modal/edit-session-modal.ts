import { Component, ElementRef, inject, ViewChild } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { SessionModel } from '../../models/session-model';
import { SessionService } from '../../services/session-service';
import { futureDateValidator } from '../../misc/future-date-validator';
import { endAfterStartValidator } from '../../misc/end-time-validator';

@Component({
  selector: 'app-edit-session-modal',
  imports: [ReactiveFormsModule],
  templateUrl: './edit-session-modal.html',
  styleUrl: './edit-session-modal.css',
})
export class EditSessionComponent {
  sessionService = inject(SessionService);
  @ViewChild('closeButton') closeButton!: ElementRef<HTMLButtonElement>;
  editSessionForm: FormGroup;
  currentSessionId: number | null = null;

  constructor(private fb: FormBuilder) {
    this.editSessionForm = this.fb.group(
      {
        sessionName: ['', [Validators.required]],
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

  public get sessionName() {
    return this.editSessionForm.get('sessionName');
  }

  public get date() {
    return this.editSessionForm.get('date');
  }

  public get startTime() {
    return this.editSessionForm.get('startTime');
  }

  public get endTime() {
    return this.editSessionForm.get('endTime');
  }

  public get sessionLink() {
    return this.editSessionForm.get('sessionLink');
  }

  openModal(session: SessionModel) {
    this.currentSessionId = session.sessionId;
    this.editSessionForm.patchValue(session);
  }

  onSave() {
    if (this.editSessionForm.valid) {
      if (this.currentSessionId != null && this.editSessionForm.valid) {
        this.sessionService
          .editSession(this.currentSessionId, this.editSessionForm.value)
          .subscribe({
            next: () => {
              this.closeButton.nativeElement.click();
            },
            error: (error) => {
              console.log(error);
            },
          });
      }
    }
  }
}
