import { Component, OnInit } from '@angular/core';
import { StudentService } from './student.service';
import { Student } from './models/student.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Student Management';
  students: Student[] = [];
  selectedStudent: Student | null = null;

  constructor(private studentService: StudentService) {}

  ngOnInit(): void {
    this.getStudents();
  }

  getStudents(): void {
    this.studentService.getAllStudents().subscribe(
      students => this.students = students,
      error => console.error('Error fetching students', error)
    );
  }

  showAddForm(): void {
    this.selectedStudent = { id: 0, name: '', age: 0, email: '' };
  }


  editStudent(student: Student): void {
    this.selectedStudent = { ...student };
  }

  saveStudent(): void {
    if (this.selectedStudent!.id) {
      this.studentService.updateStudent(this.selectedStudent!).subscribe(
        () => {
          this.getStudents();
          this.selectedStudent = null;
        },
        error => console.error('Error updating student', error)
      );
    } else {
      this.studentService.addStudent(this.selectedStudent!).subscribe(
        () => {
          this.getStudents();
          this.selectedStudent = null;
        },
        error => console.error('Error adding student', error)
      );
    }
  }

  deleteStudent(id: number): void {
    this.studentService.deleteStudent(id).subscribe(
      () => this.getStudents(),
      error => console.error('Error deleting student', error)
    );
  }

  cancel(): void {
    this.selectedStudent = null;
  }
}
