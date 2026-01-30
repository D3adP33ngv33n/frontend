import { Component, HostListener, inject } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-navbar',
  imports: [CommonModule, RouterModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  isScrolled = false;
  private router = inject(Router);
  
  @HostListener('window:scroll', [])
  onWindowScroll() {
    this.isScrolled = window.pageYOffset > 20;
  }

  viewBlog(): void {
    this.router.navigate(['/blog']);
  }
  
  // Handle navigation within home page
  navigateToFragment(fragment: string): void {
    // If we're on the home page, just scroll
    if (this.router.url === '/') {
      const element = document.getElementById(fragment);
      if (element) {
        element.scrollIntoView({ behavior: 'smooth', block: 'start' });
        // Update URL hash without reloading
        window.history.replaceState(null, '', `/#${fragment}`);
      }
    } else {
      this.router.navigate(['/'], { fragment: fragment });
    }
  }
}
