import { Router } from '@angular/router';
import { Component, HostListener, inject } from '@angular/core';

@Component({
  selector: 'app-navbar',
  imports: [],
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
    console.log(9999);
    
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
