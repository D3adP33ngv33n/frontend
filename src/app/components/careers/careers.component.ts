// careers.component.ts
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

interface JobPosition {
  id: number;
  code: string;
  title: string;
  level: string;
  location: string;
  type: string;
}

@Component({
  selector: 'app-careers',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './careers.component.html',
  styleUrl: './careers.component.scss'
})
export class CareersComponent {
  currentOpenings: JobPosition[] = [
    {
      id: 1,
      code: 'CS-01',
      title: 'Security Analyst',
      level: 'Senior',
      location: 'Remote',
      type: 'Full-time'
    },
    {
      id: 2,
      code: 'CS-02',
      title: 'Penetration Tester',
      level: 'Mid',
      location: 'Any Office',
      type: 'Full-time'
    },
    {
      id: 3,
      code: 'CS-03',
      title: 'Cryptographic Researcher',
      level: 'Senior',
      location: 'Remote',
      type: 'Contract'
    }
  ];

  constructor(private router: Router) {}

  viewDetails(jobId: number): void {
    this.router.navigate(['/careers', jobId]);
  }
}