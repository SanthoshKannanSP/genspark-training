import { Component, ElementRef, inject, Input, ViewChild } from '@angular/core';
import { SessionModel } from '../models/session-model';
import { NoteService } from '../services/note-service';
import { SessionNotesModel } from '../models/session-notes-model';
declare var bootstrap: any;

@Component({
  selector: 'app-session-notes-modal',
  imports: [],
  templateUrl: './session-notes-modal.html',
  styleUrl: './session-notes-modal.css',
})
export class SessionNotesModal {
  session: SessionModel = new SessionModel();
  uploadedNotes: SessionNotesModel[] = [];
  noteService = inject(NoteService);

  @Input() editable: boolean = false;

  @ViewChild('fileInput') fileInput!: ElementRef<HTMLInputElement>;

  constructor() {
    this.noteService.notes$.subscribe({
      next: (notes) => (this.uploadedNotes = notes),
      error: (error) => console.log(error),
    });
  }

  viewNote(noteId: number) {
    this.noteService.getNote(noteId).subscribe({
      next: (blob) => {
        const url = window.URL.createObjectURL(blob as Blob);
        window.open(url, '_blank');
        setTimeout(() => {
          window.URL.revokeObjectURL(url);
        }, 2000);
      },
      error: (error) => console.log(error),
    });
  }

  openModal(session: SessionModel) {
    this.session = session;
    this.noteService.getSessionNote(this.session.sessionId);
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length) {
      this.noteService.uploadNotes(input.files, this.session.sessionId);
      input.value = '';
    }
  }

  removeNote(index: number): void {
    if (
      confirm(
        `Are you sure you want to delete the note ${this.uploadedNotes[index].noteName}? This action cannot be undone.`
      )
    ) {
      this.noteService.deleteNote(this.uploadedNotes[index].noteId).subscribe({
        next: (data) => this.noteService.getSessionNote(this.session.sessionId),
        error: (error) => console.log(error),
      });
    }
  }

  triggerFileInput(): void {
    this.fileInput.nativeElement.click();
  }
}
