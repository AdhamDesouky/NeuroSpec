**Recover360 - Physiotherapy Clinic Management System**

## Project Description

**Recover360** is an advanced desktop application developed in C# using the Windows Presentation Foundation (WPF) framework. It is designed to streamline and enhance the operations of physiotherapy clinics, providing a comprehensive solution for efficient patient management, scheduling, and clinic administration. Leveraging Visual Studio and essential libraries compatible with Windows operating systems, Recover360 offers a user-friendly interface and key features for effective healthcare management.

### Key Features:

1. **User-Friendly Interface:**
   - Intuitive interface dynamically adapting to admin, physician, or receptionist view based on user credentials.

2. **Role-Based Access Control:**
   - Strict role-based access control ensures users access only relevant information based on their role.

3. **Centralized Dashboard:**
   - Main dashboard post-login provides a centralized overview with names and statuses of physicians, employee lists, today's appointments, and active appointments.
   - Dynamic search bar enhances navigation.

4. **Patient Management:**
   - Seamless patient management with features for retrieving comprehensive patient data and adding new patients effortlessly.
   - Captures basic demographics, medical history, progress notes, diagnostic results, and outcome measurements.

5. **Calendar Integration:**
   - Comprehensive calendar page integrates with electronic health records, offering a detailed view of scheduled appointments.
   - Ensures efficient appointment administration with reminders and notifications.

6. **Financial Management:**
   - Streamlined billing processes for generating invoices for patient sessions and services.

7. **Reporting and Analytics:**
   - Reporting module provides detailed analytics on clinic operations, including patient outcomes, appointment trends, and revenue.
   - Visual representations aid in data-driven decision-making.

8. **Communication and Notifications:**
   - Internal communication facilitated through a messaging feature.
   - Automated notifications keep staff well-informed about appointments.

9. **OCR Integration:**
   - Seamless integration of Optical Character Recognition (OCR) technology for extracting textual information from documents like X-ray images or prescriptions.

10. **Inventory Management:**
    - Functionality for managing clinic inventory, tracking, and updating stock levels of physiotherapy equipment, supplies, and resources.

11. **User Authentication and Access Control:**
    - Each user is assigned a unique ID, password, and username for secure authentication.
    - Access control mechanisms ensure data security and compliance with healthcare privacy regulations.

12. **Decision Support System for Physicians:**
    - Sophisticated Decision Support System (DSS) designed to assist physicians in making informed clinical decisions.
    - DSS analyzes patient data, treatment plans, and historical information, providing valuable insights and recommendations.

### Each View Description:

#### Admin View:

The Admin View provides comprehensive control over the physiotherapy clinic's operations. Admins access a centralized dashboard offering real-time information on active physicians, staff, and appointments. They can efficiently manage patient data, oversee treatment plans, and navigate through various sections using an intuitive interface. With the ability to add physicians, schedule appointments, and monitor overall clinic activities, the Admin View ensures seamless administration and effective decision-making.

#### Physician View:

Physicians experience a tailored interface within Recover360, focusing on essential functionalities for patient care. The dashboard showcases active appointments, patient details, and ongoing treatments. Physicians can add new patients, update medical records, and input progress notes. The system facilitates quick access to relevant patient information, optimizing the workflow for diagnosis and treatment. The Physician View ensures a streamlined experience, allowing physicians to concentrate on delivering quality care.

#### Reception View:

Designed for front desk efficiency, the Reception View prioritizes schedule management and patient interactions. Receptionists can easily add new patients, schedule appointments, and view the daily schedule. The interface emphasizes accessibility, enabling quick retrieval of patient information for check-ins and updates. With features tailored to front desk responsibilities, the Reception View enhances the clinic's overall organizational effectiveness and patient satisfaction.






UML Diagram:

![Image](Images/UML.png)

Front-end explanations:
Upon initiation of the Recover360 application, users encounter a user-friendly interface designed for intuitive navigation. The primary screen serves as the entry point, prompting users to log in. Upon entering their ID, the interface dynamically adapts to either the admin, physician, or reception view.
 
![Image](Images/fig1.jpg)
Let’s take an example of the user entering the admin view, Figure 2 depicts the main dashboard post-login, providing a centralized overview. It includes the names and statuses of physicians actively working today, a list of employees, todays, and active appointments. This hub empowers the admin to swiftly access vital information and seamlessly navigate different sections of the application. Additionally, the home page features a search bar, enabling users to efficiently locate specific physicians or appointments, contributing to an enhanced navigation experience.
 
![Image](Images/fig2.jpg)
Physicians are equipped with an "Add" button, opening a detailed sub-page for the admin to add physicians (Figure 3).

 
![Image](Images/fig3.jpg)
Furthermore, there's an "Add Appointment" button (Figure 4), facilitating manual or automatic reservation of appointments for patients based on room and physician availability.
 
![Image](Images/fig4.jpg)
To initiate the information retrieval process, entering the patient's ID or name on the landing page promptly retrieves the patient's comprehensive data, presenting it on the dedicated "Patient Integrated View" page (Figure 5).
 
![Image](Images/fig5.jpg)

Adding a new patient is seamlessly designed. Navigating and selecting "Add Patient" opens a window tailored for admin use. Intuitive design elements make it effortless to input patient details. Clicking "Save" not only confirms the addition but also provides validation feedback for assurance.  
![Image](Images/fig6.jpg)
Within the patient view (Figure 7), the admin can conveniently review the patient's Overview, encompassing crucial details like medical records, screening results, and active treatment plans.
 
![Image](Images/fig7.jpg)

For a deeper dive into the patient's medical history, the "Medical Records" tab provides access to detailed information such as lab results, screening outcomes, and summaries of past visits. To ensure up-to-date patient information, the "Active Treatment Plan" tab empowers modifications to medications or other treatment aspects. Accessing valuable insights from the patient's history is simplified through the "Previous Visits" tab, offering a consolidated view of past visit summaries.
A thorough medical history is recorded. Medications, allergies, and other relevant medical information are also documented. Physical therapists conduct initial evaluations and initial complaints are recorded and saved in the notes.



![Image](Images/fig8.jpg)



Diagnostic test results, imaging reports, and referral data related to the patient's condition are integrated into the system for comprehensive care and treatment. (Figure 8)
 

The calendar page within the physical therapy software system offers staff members a comprehensive overview of scheduled appointments. It displays appointments by days, weeks, or months, with each entry including vital details such as appointment time, reason for the appointment, and additional instructions or notes. Users can navigate through past, present, and future appointments with ease. The system generates reminders or notifications to alert staff or providers of upcoming appointments or schedule changes, ensuring effective appointment administration.
 
![Image](Images/fig9.jpg)
Chat Page features accessible to all staff members, fostering seamless communication and collaboration within the physiotherapy clinic. The Chat Page serves as a centralized platform where administrators, physicians, and receptionists can connect in real-time. 
 
![Image](Images/fig10.jpg)

Physiotherapists access personal details, patient overviews, visit histories, and financial records effortlessly. This comprehensive dashboard facilitates efficient care delivery, allowing physiotherapists to track patient progress and manage administrative tasks seamlessly.
 
![Image](Images/fig11.jpg)
The system incorporates Optical Character Recognition (OCR) technology, enabling seamless conversion of images, including prescriptions or X-ray reports, into editable text. This ensures that critical information is readily available, fostering efficient communication and decision-making among healthcare professionals.
 
![Image](Images/fig12.jpg)
Recover360's receptionist dashboard provides a quick and focused interface. It displays active physiotherapists, today's patients for easy updates, and real-time appointment management. The dashboard ensures efficient front-desk operations, enabling receptionists to stay on top of daily clinic activities effortlessly.  
![Image](Images/fig13.jpg)
Designed for streamlined patient management, the physiotherapist dashboard offers quick access to assigned patients, today's appointments, and active appointments, ensuring efficient and focused care.
 
![Image](Images/fig14.jpg)

Efficiently view patient information, notes, prescriptions, and chief complaints on a single page. Streamlining the patient encounter for physiotherapists and physicians, this feature enhances treatment planning accuracy.  
![Image](Images/fig15.jpg)
You can click on X-ray images, and they have the option to zoom in or out.  
![Image](Images/fig16.jpg)
Advanced prescription systems allow healthcare professionals to create detailed exercise prescriptions for patients. Physiotherapists can input specific exercises, define their frequency, and add any relevant notes. The system ensures a comprehensive and organized record of prescribed exercises tailored to each patient's needs.
 ![Image](Images/fig17.jpg)

**Contribution List:**

## Abdelrahman Saleh:

- **Front End:**
  - View medical record.
  - Prescription.
  - Patient search.
  - Patient integrated view.
  - New patient page.
  - Chat page.
  - Calendar Page.
  - Admin Dashboard.

- **Backend:**
  - Implemented C# functions to deal with DB backend for classes: User, Patient, Chronic Disease, etc.
  - OCR for scanning records.
  - Scanning records.

- **Database:**
  - Classes design.
  - DB UML.
  - Wireframe.

- **Pre:**
  - Tying up some pages to easily navigate.
  - Login system.
  - Documentation.

## Adham Desouky:

- **Front End:**
  - Patient record.
  - Physiotherapist dashboard.
  - New visit page.
  - Co- calendar page.

- **Backend:**
  - Co-DB MySQL code.

- **Database:**
  - Classes design.
  - Wireframe.
  - Documentation.

## Pola Alaa:

- **Front End:**
  - Receptionist dashboard.
  - Co- New patient page.
  - Login page.

- **Backend:**
  - Tying up some pages to easily navigate.
  - Created DB MySQL code based on UML design.

- **Database:**
  - Classes design.
  - DB UML.
  - UML Design.

## Hazem Swelam:

- **Front End:**
  - View physiotherapist Info.
  - New physiotherapist page.
  - New employee page.
  - Co- chat page.

- **Backend:**
  - Tying up some pages to easily navigate.

- **Database:**
  - Classes design.
  - Wireframe.

## John Albear:

- **Front End:**
  - View Image.
  - Co- patient integrated view.
  - Co- visit page.

- **Backend:**
  - Tying up some pages to easily navigate.

- **Database:**
  - Classes design.
  - Wireframe.

**Note:** Most contributions were done on a local level and not pushed directly onto GitHub due to previous issues with merge conflicts and crashes in Visual Studio. Commits were made through the team leader's device to ensure project stability.
