import { Component, OnInit, inject, input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

interface JobDetail {
  id: number;
  code: string;
  title: string;
  level: string;
  type: string;
  location: string;
  responsibilities: string[];
  requiredSkills: string[];
  preferredSkills: string[];
  benefits: string[];
}

@Component({
  selector: 'app-career-detail',
  imports: [],
  templateUrl: './career-detail.component.html',
  styleUrl: './career-detail.component.scss'
})
export class CareerDetailComponent implements OnInit {
  // Using new input() syntax from Angular 16+
  id = input<string>('');
  
  job: JobDetail | null = null;
  applyForm: FormGroup;
  submitted = false;
  
  private router = inject(Router);
  private fb = inject(FormBuilder);
  
  jobDetails: JobDetail[] = [
    {
      id: 1,
      code: 'CS-01',
      title: 'Security Analyst',
      level: 'Senior',
      location: 'Remote',
      type: 'Full-time',
      responsibilities: [
        'Monitor network traffic for anomalies',
        'Investigate security incidents',
        'Implement security controls',
        'Generate threat intelligence reports'
      ],
      requiredSkills: [
        '5+ years in security operations',
        'SIEM platform experience',
        'Network security knowledge',
        'Incident response procedures'
      ],
      preferredSkills: [
        'Scripting (Python/Bash)',
        'Cloud security (AWS/Azure)',
        'Threat hunting experience',
        'Security certifications'
      ],
      benefits: [
        'Full remote work',
        'Security training budget',
        'Cutting-edge tools',
        'Health insurance'
      ]
    },
    {
      id: 2,
      code: 'CS-02',
      title: 'Penetration Tester',
      level: 'Mid',
      location: 'Any Office',
      type: 'Full-time',
      responsibilities: [
        'Conduct authorized penetration tests',
        'Identify vulnerabilities in systems',
        'Write detailed assessment reports',
        'Develop exploitation tools'
      ],
      requiredSkills: [
        '3+ years penetration testing',
        'Web/mobile/network testing',
        'OWASP Top 10 knowledge',
        'Report writing skills'
      ],
      preferredSkills: [
        'Red team experience',
        'Binary analysis/reversing',
        'Coding (Python/Ruby)',
        'OSCP or similar certs'
      ],
      benefits: [
        'Flexible location',
        'Research time allocation',
        'Conference attendance',
        'Equipment allowance'
      ]
    }
  ];

  constructor() {
    this.applyForm = this.fb.group({
      fullName: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      linkedin: [''],
      cv: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    const jobId = Number(this.id());
    this.job = this.jobDetails.find(job => job.id === jobId) || null;
    
    if (!this.job) {
      this.goBack();
    }
  }

  goBack(): void {
    this.router.navigate(['/'], { fragment: 'careers' });
  }

  onSubmit(): void {
    if (this.applyForm.valid && this.job) {
      this.submitted = true;
      
      // In real app, you would send this to your backend
      const application = {
        ...this.applyForm.value,
        jobCode: this.job.code,
        jobTitle: this.job.title
      };
      
      console.log('Application submitted:', application);
      
      // Simulate API call
      setTimeout(() => {
        alert(`Application for ${this.job?.title} submitted successfully. We will contact you through secure channels.`);
        this.applyForm.reset();
        this.submitted = false;
        this.goBack();
      }, 1000);
    }
  }
}
