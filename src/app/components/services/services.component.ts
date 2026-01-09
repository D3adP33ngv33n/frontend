import { Component } from '@angular/core';

@Component({
  selector: 'app-services',
  imports: [],
  templateUrl: './services.component.html',
  styleUrl: './services.component.scss'
})
export class ServicesComponent {
  services = [
    {
      title: 'Silent Protection',
      description: 'Continuous monitoring without detection. We watch without being seen.',
      icon: '👁️'
    },
    {
      title: 'Data Obfuscation',
      description: 'Making your sensitive information unreadable to unauthorized entities.',
      icon: '🌀'
    },
    {
      title: 'Threat Neutralization',
      description: 'Eliminating digital threats before they become aware of our presence.',
      icon: '⚡'
    }
  ];
}
