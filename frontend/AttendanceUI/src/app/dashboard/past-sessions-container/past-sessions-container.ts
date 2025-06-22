import { Component } from '@angular/core';
import { PastSessionCard } from '../past-session-card/past-session-card';

@Component({
  selector: 'app-past-sessions-container',
  imports: [PastSessionCard],
  templateUrl: './past-sessions-container.html',
  styleUrl: './past-sessions-container.css',
})
export class PastSessionsContainer {
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
  ];
}
