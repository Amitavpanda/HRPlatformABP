import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '@abp/ng.core';
import { jwtDecode } from 'jwt-decode';
import { AttendanceLogService } from '../../proxy/attendance-logs/attendance-log.service';
import { AttendanceLogCreateDto } from '../../proxy/attendance-logs/models';
import { EmployeeService } from '../../proxy/employees/employee.service';

@Component({
  selector: 'app-attendance-logs',
  templateUrl: './attendance-logs.component.html',
  styleUrls: ['./attendance-logs.component.scss'],
  standalone: true,
  imports: [ReactiveFormsModule],
})
export class AttendanceLogsEmployeeComponent implements OnInit {
  attendanceForm: FormGroup;
  userId: string | undefined;
  userName: string | undefined;
  employeeId: string | undefined;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private attendanceLogService: AttendanceLogService,
    private employeeService: EmployeeService
  ) {
    this.attendanceForm = this.fb.group({
      date: ['', Validators.required],
      checkInTime: ['', Validators.required],
      checkOutTime: ['', Validators.required],
      status: ['', Validators.required],
    });

    const token = this.authService.getAccessToken();
    if (token) {
      const decodedToken: any = jwtDecode(token);
      console.log('Decoded Token:', decodedToken); // Log the token to verify its structure
      this.userId = decodedToken.sub; // Typically `sub` contains the user ID
      this.userName = decodedToken.unique_name; // Adjust based on your token structure
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
    if (this.attendanceForm.valid && this.employeeId) {
      const attendanceLog: AttendanceLogCreateDto = {
        ...this.attendanceForm.value,
        employeeId: this.employeeId,
      };

      console.log('Form Values:', this.attendanceForm.value); // Log the form values being passed from the frontend
      console.log('Attendance Log:', attendanceLog); // Log the complete attendance log object

      this.attendanceLogService.create(attendanceLog).subscribe(
        (response) => {
          console.log('Attendance Log saved successfully:', response);
          this.attendanceForm.reset(); // Reset the form values after successful submission
        },
        (error) => {
          console.error('Error saving Attendance Log:', error);
        }
      );
    }
  }
}
