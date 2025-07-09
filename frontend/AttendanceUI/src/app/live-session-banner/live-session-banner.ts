import { Component, inject, Input } from '@angular/core';
import { Router } from '@angular/router';
import { LiveSessionService } from '../services/live-session-service';
import { LiveSessionModel } from '../models/live-session-model';

@Component({
  selector: 'app-live-session-banner',
  imports: [],
  templateUrl: './live-session-banner.html',
  styleUrl: './live-session-banner.css',
})
export class LiveSessionBanner {
  liveSession!: LiveSessionModel;
  router = inject(Router);
  liveSessionService = inject(LiveSessionService);

  constructor() {
    this.liveSessionService.liveSessionDetails$.subscribe({
      next: (data) => (this.liveSession = data),
      error: (error) => console.log(error),
    });
    this.liveSessionService.updateLiveSession();
  }

  goToControl() {
    this.router.navigateByUrl('/session/live');
  }
}
