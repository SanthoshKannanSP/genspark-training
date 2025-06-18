import { Component, inject } from '@angular/core';
import { UserModel } from '../models/user-model';
import { UserService } from '../services/user-service';
import {
  ArcElement,
  BarController,
  BarElement,
  CategoryScale,
  Chart,
  DoughnutController,
  Legend,
  LinearScale,
  Tooltip,
} from 'chart.js';

@Component({
  selector: 'app-role-bar-chart',
  imports: [],
  templateUrl: './role-bar-chart.html',
  styleUrl: './role-bar-chart.css',
})
export class RoleBarChart {
  userService = inject(UserService);
  chart!: any;
  data!: UserModel[];

  constructor() {
    this.userService.users$.subscribe({
      next: (data) => ((this.data = data), this.updateChart()),
    });
    Chart.register(
      BarController,
      Legend,
      Tooltip,
      CategoryScale,
      LinearScale,
      BarElement
    );
  }

  updateChart() {
    let adminCount = this.data.filter((user) => user.role == 'admin').length;
    let userCount = this.data.filter((user) => user.role == 'user').length;
    let modCount = this.data.filter((user) => user.role == 'moderator').length;
    if (this.chart) {
      this.chart.destroy();
    }
    this.chart = new Chart('role-bar', {
      type: 'bar',
      options: {
        plugins: {
          legend: {
            display: false,
          },
          tooltip: {
            enabled: true,
            callbacks: {
              label: function (context) {
                const value = context.raw;
                return `${value}`;
              },
            },
          },
        },
      },
      data: {
        labels: ['Admin', 'Moderator', 'User'],
        datasets: [
          {
            data: [adminCount, modCount, userCount],
            backgroundColor: [
              'rgb(255, 99, 132)',
              'rgb(54, 162, 235)',
              'rgb(255, 205, 86)',
            ],
          },
        ],
      },
    });
  }
}
