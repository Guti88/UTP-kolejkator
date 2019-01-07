# Queue system for deanery


**<p align="center">UNIWERSYTET TECHNOLOGICZNO-PRZYRODNICZY</p>**
**<p align="center">IM. J. I J. ŚNIADECKICH W BYDGOSZCZY</p>**

<p align="center">
  <img src="https://i.imgur.com/5GFMLS5.png"/>
</p>

**<p align="center">WYDZIAŁ TELEKOMUNIKACJI, INFORMATYKI I ELEKTROTECHNIKI</p>**
**<p align="center">ZAKŁAD TECHNIKI CYFROWEJ</p>**

*<p align="center">University of Science and Technology 2018</p>*
 
## Project structure
**SQL Database:**
* Field
  * idField
  * Name
  * isDaily
  * isMaster
* Student
  * idStudent
  * Name
  * Surname
  * IndexNumber
  * RFID
* Queue
  * idQueue
  * Status
  
  **Web Service**
  * Database communication (database operations)
  * Communication with other system components (POST; GET requests + JSON)
  * Checking if communication is allowed
  
  **Web Application**
  * Displaying status of all queues
  * Displaying status of selected queue
  
  **Raspberry PI RFID scanner**
  * Scanning student card for ID
  * Sending a request to enter scanned ID into selected queue
  * Receiving status of said request
  
  **Desktop Application**
  * Sending a request to halt queue
  * Sending a request to delete selected ID from queue
  * Receiving status of selected queue
  
  
  
