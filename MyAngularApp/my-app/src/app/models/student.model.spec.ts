import { Student } from './student.model';

describe('Student', () => {
  it('should create an instance', () => {
    expect(new Student(1, 'John Doe', 25, 'john.doe@example.com')).toBeTruthy();
  });
});
