import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { AuthService } from '@abp/ng.core';
import { jwtDecode } from 'jwt-decode';
import { HRManagerService } from '@proxy/hrmanagers';
@Component({
  selector: 'app-hr-manager-dashboard',
  templateUrl: './hr-manager-dashboard.component.html',
  styleUrls: ['./hr-manager-dashboard.component.scss'],
  standalone: true,
  imports: [CommonModule],
})
export class HrManagerDashboardComponent implements OnInit {
  leaveRequests: any[] = [];
    userId: string | undefined;
  userName: string | undefined;
    hrManagerId: string | undefined;
  leaveRequestTypeMap: { [key: number]: string } = {
    0: 'Sick Leave',
    1: 'Paid Leave',
    2: 'Unpaid Leave',
  };

  constructor(private http: HttpClient,
        private authService: AuthService,
        private hrManagerService: HRManagerService
        
  ) {

        const token = this.authService.getAccessToken();
        if (token) {
          const decodedToken: any = jwtDecode(token);
          console.log('Decoded Token:', decodedToken); // Log the token to verify its structure
          this.userId = decodedToken.sub; // Typically `sub` contains the user ID
          this.userName = decodedToken.unique_name;
          console.log('userID', this.userId) // Adjust based on your token structure
        }

  }


    ngOnInit() {
    if (this.userId) {
      this.hrManagerService.getHRMAnaagerByUserId(this.userId).subscribe(
        (response) => {
          console.log('HR Response:', response);
          console.log(response.items[0].hrManager.id);
          this.hrManagerId = response.items[0].hrManager.id; // Assuming the first item contains the employee ID

        },
        (error) => {
          console.error('Error fetching employeeId:', error);
        }
      );
    }
    this.fetchLeaveRequests();
  }




  fetchLeaveRequests(): void {
    this.http.get('https://localhost:44325/api/app/leave-requests/pending').subscribe(
      (response: any) => {
        this.leaveRequests = response.items;
        console.log('Leave Requests:', this.leaveRequests);
        console.log('Response of hr manager dashbaord', response.items[0]);
      },
      (error) => {
        console.error('Error fetching leave requests:', error);
      }
    );
  }

  approveLeaveRequest(id: string): void {
    if (this.hrManagerId) {
      const url = `https://localhost:44325/api/app/leave-requests/${id}/approve?hrManagerId=${this.hrManagerId}`;

      this.http.post(url, {}).subscribe(
        (response) => {
          console.log('Leave request approved successfully:', response);
          this.fetchLeaveRequests(); // Refresh the list after approval
        },
        (error) => {
          console.error('Error approving leave request:', error);
        }
      );
    } else {
      console.error('HR Manager ID is not available. Cannot approve leave request.');
    }
  }

  rejectLeaveRequest(id: string): void {
    console.log('Reject leave request:', id);
    console.log('HR Manager ID:', this.hrManagerId); 
    
        if (this.hrManagerId) {
      const url = `https://localhost:44325/api/app/leave-requests/${id}/reject?hrManagerId=${this.hrManagerId}`;

      this.http.post(url, {}).subscribe(
        (response) => {
          console.log('Leave request rejected successfully:', response);
          this.fetchLeaveRequests(); // Refresh the list after approval
        },
        (error) => {
          console.error('Error approving leave request:', error);
        }
      );
    } else {
      console.error('HR Manager ID is not available. Cannot approve leave request.');
    }
    }
}
