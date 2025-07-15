import { Component, EventEmitter, inject, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { SessionService } from '../../services/session-service';
import { Router, RouterState } from '@angular/router';
import { AttendanceService } from '../../services/attendance-service';

@Component({
  selector: 'app-filter-menu',
  imports: [ReactiveFormsModule],
  templateUrl: './filter-menu.html',
  styleUrl: './filter-menu.css',
})
export class FilterMenu implements OnInit {
  sessionService = inject(SessionService);
  attendanceService = inject(AttendanceService);
  router = inject(Router);
  route = this.router.url;

  filterForm: FormGroup;

  isFilterCollapsed: boolean = true;
  screenIsSmall = false;

  toggleFilter() {
    this.isFilterCollapsed = !this.isFilterCollapsed;
  }

  ngOnInit() {
    this.screenIsSmall = window.innerWidth < 768;
    window.addEventListener('resize', () => {
      this.screenIsSmall = window.innerWidth < 768;
      if (!this.screenIsSmall) this.isFilterCollapsed = false;
    });
  }

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
    if (this.route == '/portal/sessions') {
      this.sessionService.updateAllSessions(null, null, this.filterForm.value);
    } else if (this.route == '/portal/attendance') {
      this.attendanceService.updateAllSessions(
        null,
        null,
        this.filterForm.value
      );
    }
  }

  resetFilters() {
    this.filterForm.reset();
    this.applyFilters();
  }
}
