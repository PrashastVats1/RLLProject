import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css'],
})
export class AboutComponent implements OnInit {
  companyName: string = 'Vaccine Management System';
  missionStatement: string =
    'To ensure that every individual has timely access to vaccines and to promote global health by providing efficient and transparent vaccine management solutions.';

  teamMembers = [
    {
      name: 'Prashast Vats',
      position: 'Team Lead',
      description:
        'Prashast is the team lead for the project who worked on the Login/ Forgot Password Component and connected all the components and apis together.',
    },
    {
      name: 'Nikhil Rastogi',
      position: 'Sub-Team Lead',
      description:
        'Nikhil is the sub team lead for the project who worked on the dashboard component along with the authorization for the same.',
    },
    {
      name: 'Mohd Shakaib Ghazi',
      position: 'Team Member',
      description:
        'Shakaib is a team member who helped in creating table for user and worked on the signup component.',
    },
    {
      name: 'Nikith S',
      position: 'Team Member',
      description:
        'Nikith is a team member who helped in creating table for vaccine centers and worked on the vaccine center component.',
    },
    {
      name: 'Nagashree',
      position: 'Team Member',
      description:
        'Nagashree is a team member who helped in creating table for booking slot and worked on the booking slot component.',
    },
    {
      name: 'Pichhala Sumalatha',
      position: 'Team Member',
      description:
        'Sumalatha is a team member who helped in creating table for vaccine inventory and worked on the vaccine inventory component.',
    },
    {
      name: 'Paildar Basanthi',
      position: 'Team Member',
      description:
        'Basanthi is a team member who worked on the booking history and tested the deployment of project on docker.',
    },
  ];

  selectedMember: any = null;
  constructor() {}

  ngOnInit(): void {}
}
