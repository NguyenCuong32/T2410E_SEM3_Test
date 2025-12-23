from patient_service import add_patients
from doctor_service import add_doctors
from appointment_service import (
    add_appointments,
    appointment_report,
    appointments_today
)

def show_menu():
    while True:
        print("""
========= MEDICAL SERVICE SYSTEM =========
1. Add patients
2. Add doctors
3. Add appointments
4. Appointment report
5. Today's appointments
0. Exit
=========================================
        """)
        choice = input("Choose: ")

        if choice == "1":
            add_patients()
        elif choice == "2":
            add_doctors()
        elif choice == "3":
            add_appointments()
        elif choice == "4":
            appointment_report()
        elif choice == "5":
            appointments_today()
        elif choice == "0":
            print("Goodbye!")
            break
        else:
            print("Invalid choice!")
