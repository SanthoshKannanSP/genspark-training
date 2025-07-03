import { HttpClient } from '@angular/common/http';
import { Component, ElementRef, inject, ViewChild } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { HttpClientService } from '../services/http-client-service';

@Component({
  selector: 'app-sidebar',
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.css',
})
export class Sidebar {
  api = inject(HttpClientService);
  router = inject(Router);
  @ViewChild('sidebarCloseButton')
  sidebarCloseButton!: ElementRef<HTMLButtonElement>;

  logout() {
    this.api.logout().subscribe({
      next: (data) => {
        console.log(this.sidebarCloseButton.nativeElement.click());
        this.router.navigateByUrl('/');
      },
      error: (error) => console.log(error),
    });
  }
}
