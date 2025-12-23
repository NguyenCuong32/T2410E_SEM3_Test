import sys
from datetime import datetime
import db_connect
import medical_service

class MedicalController:
    def __init__(self):
        self.conn=db_connect.create_connection()
        if not self.conn:
            print("Failed to connect!")
            sys.exit(1)
        
    def print_border(self,width=120):
        print("-"*width)
    
    def handle_add_patient(self):
        print("\n--- INPUT 3 PATIENTS ---")
        for i in range(1, 4):
            print(f"\n[Patient {i}/3]")
            name = input(" - Full name: ")
            dob = input(" - Date of birth (YYYY-MM-DD): ")
            gender = input(" - Gender: ")
            addr = input(" - Address: ")
            phone = input(" - Phone number: ")
            email = input(" - Email: ")
            medical_service.add_patient(self.conn, name, dob, gender, addr, phone, email)
            print("Successfully saved!")

    def handle_add_doctors(self):
        print("\n--- INPUT 5 DOCTORS ---")
        for i in range(1, 6):
            print(f"\n[Doctor {i}/5]")
            name = input(" - Full name: ")
            spec = input(" - Department: ")
            phone = input(" - Phone number: ")
            email = input(" - Email: ")
            exp = input(" - Years of experience: ")
            
            medical_service.add_doctor(self.conn, name, spec, phone, email, exp)
            print("Successfully saved!")

    def handle_add_appointments(self):
        print("\n--- INPUT 3 APPOINTMENTS ---")
        for i in range(1, 4):
            print(f"\n[Appointment {i}/3]")
            try:
                p_id = input(" - ID Patient: ")
                d_id = input(" - ID Doctor: ")
                date = input(" - Date and time (YYYY-MM-DD HH:MM:SS): ")
                reason = input(" - Reason for appointment: ")
                medical_service.add_appointment(self.conn, p_id, d_id, date, reason)
                print("Successfully scheduled!")
            except Exception as e:
                print(f"Error with data: {e}")
    def handle_show_report(self):
        print("\n--- REPORT ---")
        data = medical_service.get_general_report(self.conn)
        
        self.print_border()
        print(f"{'No':<4} | {'Patient Name':<15} | {'Birthday':<12} | {'Gender':<8} | {'Address':<15} | {'Doctor Name':<15} | {'Reason':<15} | {'Date':<15}")
        self.print_border()
        
        for idx, row in enumerate(data, 1):
            date_str = str(row[6])
            dob_str = str(row[1])
            print(f"{idx:<4} | {row[0]:<15} | {dob_str:<12} | {row[2]:<8} | {row[3]:<15} | {row[4]:<15} | {row[5]:<15} | {date_str:<15}")
    def handle_show_today(self):
        today_str = datetime.now().strftime('%Y-%m-%d')
        print(f"\n--- Appointment: {today_str} ---")
        data = medical_service.get_today_apppointment(self.conn)
        self.print_border()
        print(f"{'Address':<15} | {'No':<4} | {'Patient Name':<15} | {'Birthday':<12} | {'Gender':<8} | {'Doctor Name':<15} | {'Status':<10} | {'Note':<10}")
        self.print_border()

        if not data:
            print(">> No appointments today.")
        for idx, row in enumerate(data, 1):
            dob_str = str(row[2])
            note = "" 
            print(f"{row[0]:<15} | {idx:<4} | {row[1]:<15} | {dob_str:<12} | {row[3]:<8} | {row[4]:<15} | {row[5]:<10} | {note:<10}")

    def run(self):
        while True:
            print("\n" + "="*40)
            print("   MEDICAL MANAGEMENT SYSTEM")
            print("="*40)
            print("1. Enter 3 Patients")
            print("2. Enter 5 Doctors")
            print("3. Enter 3 Appointments")
            print("4. Show General Report")
            print("5. Show Today Report")
            print("0. Exit")
            
            choice = input(">> Select an option: ")

            if choice == '1':
                self.handle_add_patients()
            elif choice == '2':
                self.handle_add_doctors()
            elif choice == '3':
                self.handle_add_appointments()
            elif choice == '4':
                self.handle_show_report()
            elif choice == '5':
                self.handle_show_today()
            elif choice == '0':
                if self.conn:
                    self.conn.close()
                print("Exiting the program. Goodbye!")
                break
            else:
                print("Invalid choice.")