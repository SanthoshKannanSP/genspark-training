import { Component, ElementRef, inject, ViewChild } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { SessionModel } from '../../models/session-model';
import { SessionService } from '../../services/session-service';

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
    this.editSessionForm = this.fb.group({
      sessionName: ['', Validators.required],
      date: ['', Validators.required],
      startTime: ['', Validators.required],
      endTime: ['', Validators.required],
      sessionLink: ['', Validators.required],
    });
  }

  openModal(session: SessionModel) {
    this.currentSessionId = session.sessionId;
    this.editSessionForm.patchValue(session);
  }

  onSave() {
    if (this.editSessionForm.valid) {
      const updatedSession = {
        id: this.currentSessionId,
        ...this.editSessionForm.value,
      };

      if (this.currentSessionId != null && this.editSessionForm.valid) {
        this.sessionService
          .editSession(this.currentSessionId, this.editSessionForm.value)
          .subscribe({
            next: (response: any) => {
              let session = response.data;
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
