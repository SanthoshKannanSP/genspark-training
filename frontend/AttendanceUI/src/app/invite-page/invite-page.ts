import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SessionService } from '../services/session-service';
import { NotificationService } from '../services/notification-service';

@Component({
  selector: 'app-invite-page',
  imports: [],
  templateUrl: './invite-page.html',
  styleUrl: './invite-page.css',
})
export class InvitePage implements OnInit {
  sessionService = inject(SessionService);
  activatedRoute = inject(ActivatedRoute);
  notificationService = inject(NotificationService);
  router = inject(Router);

  ngOnInit(): void {
    let sessionCode = this.activatedRoute.snapshot.params['sessionCode'];
    this.sessionService.addStudentToSession(sessionCode).subscribe({
      next: () => {
        this.notificationService.addNotification({
          message: 'Added to Session Successfully',
          type: 'success',
        });
      },
      error: (error) => {
        this.notificationService.addNotification({
          message: 'Invalid Session Code',
          type: 'danger',
        });
        console.log(error);
      },
    });
    this.router.navigateByUrl('/portal/dashboard');
  }
}
