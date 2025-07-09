import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { LeaveRequestService } from '../../../proxy/leave-requests/leave-request.service';
import { LeaveRequestCreateDto } from '../../../proxy/leave-requests/models';
import { AuthService } from '@abp/ng.core';
import { jwtDecode } from 'jwt-decode';
import { EmployeeService } from '@proxy/employees';

@Component({
  selector: 'app-leave-request-employee',
  templateUrl: './leave-request-employee.component.html',
  styleUrls: ['./leave-request-employee.component.scss'],
  standalone: true,
  imports: [ReactiveFormsModule],
})
export class LeaveRequestEmployeeComponent implements OnInit {
  leaveRequestForm: FormGroup;
  userId: string | undefined;
  employeeId: string | undefined;

  constructor(
    private fb: FormBuilder,
    private leaveRequestService: LeaveRequestService,
    private authService: AuthService,
    private employeeService: EmployeeService // Assuming you have an EmployeeService to fetch employee details
  ) {
    this.leaveRequestForm = this.fb.group({
      leaveRequestType: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      leaveRequestStatus: ['Pending', Validators.required],
      requestedOn: [new Date().toISOString(), Validators.required],
    });

    const token = this.authService.getAccessToken();
    if (token) {
      const decodedToken: any = jwtDecode(token);
      this.userId = decodedToken.sub;
      console.log('Decoded Token:', decodedToken);
      console.log("userid ", this.userId); // Log the token to verify its structure

    }
  }

  ngOnInit() {
    if (this.userId) {
      this.employeeService.getEmployeeByUserId(this.userId).subscribe(
        (response) => {
          console.log('Employee Response:', response);
          console.log(response.items[0].employee.id);
          this.employeeId = response.items[0].employee.id; // Assuming the first item contains the employee ID

        },
        (error) => {
          console.error('Error fetching employeeId:', error);
        }
      );
           }
  }

  onSubmit() {

    console.log('Form Submitting');
    console.log('Form Valid:', this.leaveRequestForm.valid);
    console.log('Employee ID:', this.employeeId);
    if (this.leaveRequestForm.valid && this.employeeId) {
      const leaveRequest: LeaveRequestCreateDto = {
        ...this.leaveRequestForm.value,
        employeeId: this.employeeId,
      };

      console.log('Form Values:', this.leaveRequestForm.value);
      console.log("employeeId ",leaveRequest);


      this.leaveRequestService.create(leaveRequest).subscribe(
        (response) => {
          console.log('Leave Request saved successfully:', response);
          this.leaveRequestForm.reset();
        },
        (error) => {
          console.error('Error saving Leave Request:', error);
        }
      );
    }
  }
}
