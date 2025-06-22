import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-sessions-table',
  imports: [],
  templateUrl: './sessions-table.html',
  styleUrl: './sessions-table.css',
})
export class SessionsTable implements OnInit {
  @Input() sessions: any[] = [];

  currentPage = 1;
  pageSize = 10;
  paginatedSessions: any[] = [];

  ngOnInit() {
    this.updatePaginatedSessions();
  }

  updatePaginatedSessions() {
    const start = (this.currentPage - 1) * this.pageSize;
    const end = start + this.pageSize;
    this.paginatedSessions = this.sessions.slice(start, end);
  }

  goToPage(page: number) {
    this.currentPage = page;
    this.updatePaginatedSessions();
  }

  get totalPages(): number {
    return Math.ceil(this.sessions.length / this.pageSize);
  }

  get pageNumbers(): number[] {
    return Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }
}
