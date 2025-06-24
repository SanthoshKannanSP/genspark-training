import { Component, EventEmitter, inject, Output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { SessionService } from '../../services/session-service';

@Component({
  selector: 'app-filter-menu',
  imports: [ReactiveFormsModule],
  templateUrl: './filter-menu.html',
  styleUrl: './filter-menu.css',
})
export class FilterMenu {
  sessionService = inject(SessionService);

  filterForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.filterForm = this.fb.group({
      sessionName: [''],
      status: [''],
      startDate: [''],
      endDate: [''],
      startTime: [''],
      endTime: [''],
    });
  }

  applyFilters() {
    this.sessionService.updateAllSessions(null, null, this.filterForm.value);
    console.log('Ok');
  }

  resetFilters() {
    this.filterForm.reset();
    this.applyFilters();
  }
}
