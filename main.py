from patient import add_patients
from doctor import add_doctors
from appointment import add_appointments
from report import report_all, appointments_today

while True:
    print("""
==== MEDICAL SERVICE SYSTEM ====
1. Thêm 3 bệnh nhân
2. Thêm 5 bác sĩ
3. Thêm 3 lịch hẹn
4. Báo cáo tất cả lịch hẹn
5. Lịch hẹn hôm nay
0. Thoát
""")

    choice = input("Chọn chức năng: ")

    if choice == "1":
        add_patients()
    elif choice == "2":
        add_doctors()
    elif choice == "3":
        add_appointments()
    elif choice == "4":
        report_all()
    elif choice == "5":
        appointments_today()
    elif choice == "0":
        break
    else:
        print("❌ Lựa chọn không hợp lệ")
