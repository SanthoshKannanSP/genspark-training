import { Component, inject } from '@angular/core';
import {
  ArcElement,
  Chart,
  DoughnutController,
  Legend,
  Tooltip,
} from 'chart.js';
import { UserService } from '../services/user-service';
import { UserModel } from '../models/user-model';

@Component({
  selector: 'app-gender-pie-chart',
  imports: [],
  templateUrl: './gender-pie-chart.html',
  styleUrl: './gender-pie-chart.css',
})
export class GenderPieChart {
  userService = inject(UserService);
  chart!: any;
  data!: UserModel[];

  constructor() {
    this.userService.users$.subscribe({
      next: (data) => ((this.data = data), this.updateChart()),
    });
    Chart.register(DoughnutController, ArcElement, Legend, Tooltip);
  }

  updateChart() {
    let maleCount = this.data.filter((user) => user.gender == 'male').length;
    let femaleCount = this.data.filter(
      (user) => user.gender == 'female'
    ).length;
    if (this.chart) {
      this.chart.destroy();
    }
    this.chart = new Chart('gender-pie', {
      type: 'doughnut',
      options: {
        plugins: {
          legend: {
            display: true,
          },
          tooltip: {
            enabled: true, // ensures it's on
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
        labels: ['Male', 'Female'],
        datasets: [
          {
            data: [maleCount, femaleCount],
            backgroundColor: ['rgb(255, 99, 132)', 'rgb(54, 162, 235)'],
            hoverOffset: 4,
          },
        ],
      },
    });
  }
}
