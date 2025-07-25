import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet, RouterLink, Router, ActivatedRoute, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs';

@Component({
  selector: 'app-admin-dashboard',
  imports: [CommonModule, RouterOutlet, RouterLink],
  templateUrl: './admin-dashboard.html',
  styleUrl: './admin-dashboard.css'
})
// export class AdminDashboard {
//   isDashboardRoot: boolean = true;

//   constructor(private router: Router) {
//     this.router.events.pipe(
//       filter(event => event instanceof NavigationEnd)
//     ).subscribe((event: any) => {
//       this.isDashboardRoot = event.url === '/portal/admin-dashboard';
//     });
//   }
// }

export class AdminDashboard {
  isDashboardRoot: boolean = false;

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.checkIfRoot();
    this.router.events.subscribe(() => this.checkIfRoot());
  }

  private checkIfRoot() {
    this.isDashboardRoot = this.router.url === '/portal/admin-dashboard';
  }
}