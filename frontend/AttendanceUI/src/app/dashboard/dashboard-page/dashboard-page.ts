import { Component, ViewChild } from '@angular/core';
import { UpcomingSessionsContainer } from '../upcoming-sessions-container/upcoming-sessions-container';
import { PastSessionsContainer } from '../past-sessions-container/past-sessions-container';
import { WelcomeTitle } from '../welcome-title/welcome-title';

@Component({
  selector: 'app-dashboard-page',
  imports: [UpcomingSessionsContainer, PastSessionsContainer, WelcomeTitle],
  templateUrl: './dashboard-page.html',
  styleUrl: './dashboard-page.css',
})
export class DashboardPage {}
