import { Component } from '@angular/core';
import { UpcomingSessionCard } from '../upcoming-session-card/upcoming-session-card';

@Component({
  selector: 'app-upcoming-sessions-container',
  imports: [UpcomingSessionCard],
  templateUrl: './upcoming-sessions-container.html',
  styleUrl: './upcoming-sessions-container.css',
})
export class UpcomingSessionsContainer {
  sessions = [
    {
      title: 'Angular Basics Workshop',
      date: 'June 25, 2025',
      time: '10:00 AM - 11:30 AM',
    },
    {
      title: 'TypeScript Deep Dive',
      date: 'June 26, 2025',
      time: '12:00 PM - 1:30 PM',
    },
    // You can have less than 3; layout remains consistent
  ];
}
