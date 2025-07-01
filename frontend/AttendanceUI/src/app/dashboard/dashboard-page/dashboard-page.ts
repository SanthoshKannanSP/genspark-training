import { Component, ViewChild } from '@angular/core';
import { Sidebar } from '../../sidebar/sidebar';
import { UpcomingSessionsContainer } from '../upcoming-sessions-container/upcoming-sessions-container';
import { PastSessionCard } from '../past-session-card/past-session-card';
import { PastSessionsContainer } from '../past-sessions-container/past-sessions-container';
import { WelcomeTitle } from '../welcome-title/welcome-title';
import { SessionDetails } from '../session-details/session-details';
import { SessionModel } from '../../models/session-model';

@Component({
  selector: 'app-dashboard-page',
  imports: [
    UpcomingSessionsContainer,
    PastSessionsContainer,
    WelcomeTitle,
    SessionDetails,
  ],
  templateUrl: './dashboard-page.html',
  styleUrl: './dashboard-page.css',
})
export class DashboardPage {}
