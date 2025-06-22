import { Component } from '@angular/core';
import { Sidebar } from '../sidebar/sidebar';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-portal-page',
  imports: [Sidebar, RouterOutlet],
  templateUrl: './portal-page.html',
  styleUrl: './portal-page.css',
})
export class PortalPage {}
