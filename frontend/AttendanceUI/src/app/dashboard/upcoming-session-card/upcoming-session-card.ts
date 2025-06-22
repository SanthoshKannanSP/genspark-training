import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-upcoming-session-card',
  imports: [],
  templateUrl: './upcoming-session-card.html',
  styleUrl: './upcoming-session-card.css',
})
export class UpcomingSessionCard {
  @Input() title = '';
  @Input() date = '';
  @Input() time = '';
}
