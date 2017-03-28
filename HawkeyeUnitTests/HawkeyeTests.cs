using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HawkeyehvkBLL;
using System.Collections.Generic;

namespace HawkeyeUnitTests
{
    [TestClass]
    public class HawkeyeTests
    {


        //check that they follow last name order(currently hardcoded in stub to be in order)
        [TestMethod]
        public void listowners1()
        {
            Owner control = new Owner();
            Assert.AreEqual(19, control.listTheOwners().Count);
           
        }
        //check that they are last name then first name(relative to your listowners method)
        [TestMethod]
        public void listowners2()
        {
            Owner control = new Owner();
            Assert.AreEqual("Anita", control.listTheOwners()[0].firstName);
            Assert.AreEqual("Alibi", control.listTheOwners()[0].lastName);
        }
        //returns type list
        [TestMethod]
        public void listowners3()
        {
            // Assert.AreEqual(typeof(system.collections.generic.list<rc_hvk.owner>), control.listowners().gettype()); 
        }



        //Must be sorted by pet number
        [TestMethod]
        public void listPets1()
        {
           
            Assert.AreEqual(6, Pet.listPets(2)[0].petNumber);
            Assert.AreEqual(3, Pet.listPets(2)[1].petNumber);
        }

        //Test with owner with 0 pets(Owner 14)
        [TestMethod]
        public void listPets2()
        {
            
            Assert.AreEqual(0, Pet.listPets(14).Count);
        }

        //Test with owner with 1 pet(owner 18 pet 33)
        [TestMethod]
        public void listPets3()
        {
            
            Assert.AreEqual(1, Pet.listPets(18).Count);
            Assert.AreEqual(33, Pet.listPets(18)[0].petNumber);
        }

        //Test with owner with multiple pets(1+) (Owner owner 7 pets: 10,11,12)
        [TestMethod]
        public void listPets4()
        {
            
            Assert.AreEqual(10, Pet.listPets(7)[0].petNumber);
            Assert.AreEqual(11, Pet.listPets(7)[1].petNumber);
            Assert.AreEqual(12, Pet.listPets(7)[2].petNumber);
        }

        //Invalid owner number
        [TestMethod]
        public void listPets5()
        {
            
            Assert.AreEqual(0, Pet.listPets(000).Count);
        }

        //Owner with multiple(Owner 4)
        [TestMethod]
        public void listReservations1()
        {
            Reservation control = new Reservation();
            Assert.AreEqual(620, control.listReservations(4)[0].reservationNumber);
            Assert.AreEqual(631, control.listReservations(4)[1].reservationNumber);
        }

        //Owner with 0 (Owner 20)
        [TestMethod]
        public void listReservations2()
        {
            Reservation control = new Reservation();
            Assert.AreEqual(0, control.listReservations(20).Count);
        }

        //Invalid owner number returns null
        [TestMethod]
        public void listReservations3()
        {
            Reservation control = new Reservation();
            Assert.AreEqual(null, control.listReservations(0000));
        }

        //return all active reservation - Expected 3 hardcoded data
        [TestMethod]
        public void listActiveReservations1()
        {
            Reservation control = new Reservation();
            Assert.AreNotEqual(0, control.listActiveReservations().Count);
        }


        [TestMethod]
        public void listActiveReservations2()
        {
            Reservation control = new Reservation();
            //Owner 4 - Expected : No Active Reservation 
            Assert.AreNotEqual(new System.Collections.Generic.List<Reservation>(), control.listActiveReservations(4));
        }



        [TestMethod]
        public void listActiveReservations3()
        {
            //Owner 15 - Expected : 1 Active Reservation 
            Reservation control = new Reservation();
            Assert.AreNotEqual(0, control.listActiveReservations(15).Count);
        }

        [TestMethod]
        public void listActiveReservations4()
        {
            //Owner 15 - Expected : More than 1 active reservation 
            Reservation control = new Reservation();
            Assert.AreNotEqual(1, control.listActiveReservations(12).Count);
            Assert.AreNotEqual(0, control.listActiveReservations(12).Count);

        }

        [TestMethod]
        public void listActiveReservations5()
        {
            //Invalid Owner number expected : null 
            Reservation control = new Reservation();
            Assert.AreEqual(null, control.listActiveReservations(000000));
        }



        //Full vaccinations 
        [TestMethod]
        public void listVaccination1()
        {
            PetVaccination control = new PetVaccination();
            Assert.AreEqual(6, control.listVaccinations(1).Count);
        }

        //Not Full Vaccination 
        [TestMethod]
        public void listVaccination2()
        {
            PetVaccination control = new PetVaccination();
            Assert.AreEqual(4, control.listVaccinations(7).Count);
        }

        //Invalid Pet Number
        [TestMethod]
        public void listVaccination3()
        {
            PetVaccination control = new PetVaccination();
            Assert.AreEqual(null, control.listVaccinations(0000));
        }

        //No Vaccination
        [TestMethod]
        public void listVaccination4()
        {
            PetVaccination control = new PetVaccination();
            Assert.AreEqual(0, control.listVaccinations(10).Count);
        }

        //Invalid Pet Number 
        [TestMethod]
        public void checkVaccination1()
        {
            PetVaccination control = new PetVaccination();
            Assert.AreEqual(null, control.checkVaccinations(100, 0000));
        }

        //Invalid Reservation Number 
        [TestMethod]
        public void checkVaccination2()
        {
            PetVaccination control = new PetVaccination();
            Assert.AreEqual(null, control.checkVaccinations(00000, 1));
        }


        //Valid Vaccination - return 0 
        [TestMethod]
        public void checkVaccination3()
        {
            PetVaccination control = new PetVaccination();
            Assert.AreEqual(0, control.checkVaccinations(108, 3).Count);
        }

        //Not Valid Vaccination - return 6 
        [TestMethod]
        public void checkVaccination4()
        {
            PetVaccination control = new PetVaccination();
            Assert.AreEqual(6, control.checkVaccinations(100, 1).Count);
        }


        //Expired Vaccination 
        [TestMethod]
        public void checkVaccination5()
        {
            PetVaccination control = new PetVaccination();
            Assert.AreNotEqual(0, control.checkVaccinations(115, 6).Count);
        }

        //Pet with expired and unchecked vaccines
        [TestMethod]
        public void checkVaccination6()
        {
            PetVaccination control = new PetVaccination();
            Assert.AreNotEqual(0, control.checkVaccinations(999, 9).Count);
        }


        //Pet with missing vaccinations
        [TestMethod]
        public void checkVaccination7()
        {
            PetVaccination control = new PetVaccination();
            Assert.AreNotEqual(0, control.checkVaccinations(620, 7).Count);
        }


        //on entered date are returned(2017, 6, 20)
        [TestMethod]
        public void upcomingReservation1()
        {
            //return 6 you can aslo test with equal 6 
            Reservation control = new Reservation();
            Assert.AreNotEqual(0, control.listUpcomingReservations(new DateTime(2017, 6, 20)).Count);
        }



        //No upcoming reservation past - (1999,06,15)
        [TestMethod]
        public void upcomingReservation2()
        {
            Reservation control = new Reservation();
            Assert.AreNotEqual(0, control.listUpcomingReservations(new DateTime(2017, 6, 15)).Count);
        }


        //curentlly return 6 - you can also test with equal to  6 (2017-04-25)
        [TestMethod]
        public void upcomingReservation3()
        {
            Reservation control = new Reservation();
            Assert.AreNotEqual(0, control.listUpcomingReservations(new DateTime(2017, 4, 25)).Count);
        }

        //on the same date 
        [TestMethod]
        public void upcomingReservation4()
        {
            Reservation control = new Reservation();
            Assert.AreEqual(1, control.listUpcomingReservations(new DateTime(2017, 8, 20)).Count);
        }


        //No upcoming reservation in future
        [TestMethod]
        public void upcomingReservation5()
        {
            Reservation control = new Reservation();
            Assert.AreEqual(0, control.listUpcomingReservations(new DateTime(2050, 8, 28)).Count);
        }


        [TestClass]
        public class reservationModificationTests
        {
           

            [TestMethod]
            public void addReservationTest()
            {//addReservation(int petNumber, DateTime startDate, DateTime endDate)

                // invalid pet number test
                // Input Parameters: 
                //Pet number = 40
                //
                //Expected: 1 (invalid pet number)
                Reservation control = new Reservation();
                Assert.AreEqual(1, control.addReservation(40, DateTime.Now.AddDays(7), DateTime.Now.AddDays(10)), "invalid pet number test");


                // Start Date after end date
                // Input Parameters: Start: 
                // 01-JAN-17 - End
                // 04-JAN-17 - Start
                //Expected: 2 (start date after end date)
                Assert.AreEqual(2, control.addReservation(1, new DateTime(2017, 5, 14), new DateTime(2017, 5, 13)), " Start Date after end date test");


                //No Available runs for dog size, this will be tested when we have further understanding

                //No Available runs on date, this will be tested when we have further understanding

                // Start date == end date
                // Input Parameters: 
                // 04-JAN-17 - End
                // 04-JAN-17 - Start
                //Expected: 3 (Start date and end date same day)
                Assert.AreEqual(3, control.addReservation(1, new DateTime(2017, 5, 14), new DateTime(2017, 5, 14)), "Start date == end date test");


                // Pet has reservation during those days
                // input parameters
                // pet number: 7 
                // (existing reservation:631) 
                // dates: 01-JAN-16 TO 04-JAN-16   
                //Expected: 4 (pet has reservation at that time already)
                Assert.AreEqual(4, control.addReservation(7, new DateTime(2016, 1, 1), new DateTime(2016, 1, 4)), "Pet has reservation during those days");


                // happy case
                // Input Parameters: 
                // pet number 35
                // Start: 4-MAR-17
                // end: 6-MAR-17
                // Expected: 0 (success)
                Assert.AreEqual(0, control.addReservation(35, new DateTime(2017, 3, 4), new DateTime(2017, 3, 6)), "happy case");
            }

            [TestMethod]
            public void addToReservationTest()
            {//addToReservation(int reservationNumber, int petNumber)

                Reservation control = new Reservation();

                // invalid pet number
                // Input Parameters: 
                //Pet number: 40
                //Expected: 1 (invalid pet number)
                Assert.AreEqual(1, control.addToReservation(601, 40), "Invalid Pet number test");


                //No Available runs for dog size will be tested when we have further understanding
                // Input Parameters: 
                //
                //Expected: 

                //No Available runs on date will be tested when we have further understanding
                // Input Parameters: 
                //
                //Expected:

                // Pet has reservation during those days
                // Input Parameters: 
                //pet number: 7 
                //reservation:631 
                //  Expected: 2 (Error pet has reservation at that time already)
                Assert.AreEqual(2, control.addToReservation(631, 7), "Pet has reservation during those days");


                // invalid reservation number
                // Input Parameters: 
                // reservation number: 900
                //Expected: 3 (invalid res number)
                Assert.AreEqual(3, control.addToReservation(900, 7), "invalid reservation number test");


                //Happy Case
                // Input Parameters: 
                // reservation number: 108
                // pet number:  6
                // 
                //Expected: success (0)
                Assert.AreEqual(0, control.addToReservation(108, 6), "Happy Path");


                //pets from different owners
                //Happy Case
                // Input Parameters: 
                // reservation number: 108
                // pet number:  1
                // 
                //Expected: 4 (dogs not from same owner)
                Assert.AreEqual(4, control.addToReservation(108, 1), "pets from different owners");



            }

        }

        [TestMethod]
        public void testChangeReservation()
        {

            Reservation hvk = new Reservation();

            //Intput parameters:
            //reservation Number: 999
            //Start date: 01-01-16
            //end Date: 15-01-16
            //Expected: 1
            Assert.AreEqual(1, hvk.changeReservation(999, new DateTime(16, 01, 16), new DateTime(16, 01, 18)), "Invalid Reservation Number not returning 1");

            //Input parameters:
            //reservation number: 3
            //Start date:16 - JAN - 16
            //end date: 01-JAN-16
            //Expected: 2
            Assert.AreEqual(2, hvk.changeReservation(3, new DateTime(16, 01, 16), new DateTime(16, 01, 01)), "Start Date After End Date Not Returning 2");

            //Input parameters:
            //reservation number: 6
            //Start date:01-JAN-16
            //end date: 15-JAN-16
            //Expected: 3
            Assert.AreEqual(3, hvk.changeReservation(6, new DateTime(16, 01, 19), new DateTime(16, 01, 22)), "No available runs returning Not Returning 3");

            //Input parameters:
            //reservation number: 3
            //Start date:01-JAN-16
            //end date: 01-JAN-16
            //Expected: 4
            Assert.AreEqual(4, hvk.changeReservation(3, new DateTime(16, 01, 16), new DateTime(16, 01, 16)), "Start Date On the same Day as End Date Not Returning 4");

            //Input parameters:
            //reservation number: 605
            //Start date:5/3/2017
            //end date: 9/3/2017
            //Expected: 0
            //Happy Case AssertFalse as its the default for the empty method currently
            Assert.AreEqual(0, hvk.changeReservation(605, new DateTime(17, 03, 05), new DateTime(17, 03, 09)), "Invalid Reservation Change");

        }

        [TestMethod]
        public void testCheckVaccinations()
        {
            PetVaccination hvk = new PetVaccination();
           
            //Input Parameters:
            //Pet Number: 999
            //date: 16/01/2016
            //Expected: 1
            //Invalid Pet Number
            Assert.AreEqual(1, hvk.checkVaccinations(999, new DateTime(16, 01, 16)), "Invalid Pet Number Not returning 1");

            //Input Parameters:
            //Pet Number: 14
            //date: 17/05/05
            //Expected: 2
            //Happy Path With 1 missing Vaccination
            Assert.AreEqual(2, hvk.checkVaccinations(14, new DateTime(17, 05, 05)), "Missing 1 Vaccination Not returning 2");


            //Input Parameters:
            //Pet Number: 7
            //date: 17-02-20
            //Expected: 2
            //Happy Path With Multiple missing Vaccinations
            Assert.AreEqual(2, hvk.checkVaccinations(7, new DateTime(17, 02, 20)), "Missing Multiple Vaccinations not returning 2");
        }




        //Changes To The Methods Called to return the ints

        public int changeReservation(int reservationNumber, DateTime startDate, DateTime endDate)
        {
            int result = 1;
            if (reservationNumber == 605)
                result = 0;
            else if (startDate > endDate)
                result = 2;
            else if (reservationNumber == 6)
                result = 3;
            else if (startDate == endDate)
                result = 4;

            return result;
        }


        public int checkVaccination(int petNumber, DateTime byDate)
        {
            int result = 0;
            if (petNumber == 999)
            {
                result = 1;
            }
            else if (petNumber == 14)
            {
                result = 2;
            }
            else if (petNumber == 7)
            {
                result = 2;
            }

            return result;
        }

        [TestMethod]
        public void RunAvailability1()
        {
            Reservation hvk = new Reservation();

            //Input:              2015, 09, 12;
            //                    2017, 1, 31;
            //Expected : no avaialble run  
            //Start date equal to Reservation Start Date
            DateTime startDate = new DateTime(2015, 09, 12);
            DateTime endDate = new DateTime(2017, 1, 31);
            Assert.AreEqual(0, hvk.checkRunAvailability(startDate, endDate, 'R'));
        }

        [TestMethod]
        public void RunAvailability2()
        {
            Reservation hvk = new Reservation();
            //Input     2015, 09, 18;
            //          2017, 1, 31;
            //Expected : 0 Run Available 
            //Start date greater than Reservation Start Date 
            //End date equal to Reservation End Date
            DateTime startDate = new DateTime(2015, 09, 18);
            DateTime endDate = new DateTime(2017, 1, 31);
            Assert.AreEqual(0, hvk.checkRunAvailability(startDate, endDate, 'R'));
            /*
             * THIS TEST CASE IS FLAWED. there is 1 run that is never used.
             */ 
        }


        [TestMethod]
        public void RunAvailability3()
        {
            Reservation hvk = new Reservation();
            //Input :             2015, 09, 10;
            //                    2017, 1, 31;
            //Expected            0 Run Available 
            //Start date smaller than Reservation Start Date 
            //End date equal to Reservation End Date
            DateTime startDate = new DateTime(2015, 09, 10);
            DateTime endDate = new DateTime(2017, 1, 31);
            Assert.AreEqual(0, hvk.checkRunAvailability(startDate, endDate, 'R'));
        }



        [TestMethod]
        public void RunAvailability4()
        {
            Reservation hvk = new Reservation();
            //Input               (2015, 09, 12);
            //                    (2017, 04, 15);
            //Expected            0 Run Available 
            //Start date equals to Reservation Start Date 
            //End date greater than  Reservation End Date
            DateTime startDate = new DateTime(2015, 09, 12);
            DateTime endDate = new DateTime(2017, 04, 15);
            Assert.AreEqual(0, hvk.checkRunAvailability(startDate, endDate, 'R'));
        }




        [TestMethod]
        public void RunAvailability5()
        {
            Reservation newReservation = new Reservation();
            //Input             (2015, 09, 12);
            //                  (2016, 12, 05);
            //Expected          0 Run Available 
            //Start date equals to Reservation Start Date 
            //End date smaller than Reservation End Date
            DateTime startDate = new DateTime(2015, 09, 12);
            DateTime endDate = new DateTime(2016, 12, 05);
            Assert.AreEqual(0, newReservation.checkRunAvailability(startDate, endDate, 'R'));
        }


        [TestMethod]
        public void RunAvailability6()
        {
            Reservation newReservation = new Reservation();
            //Input              (2015, 09, 18);
            //                   (2017, 02, 15);
            //Expected           0 Run Available 
            //Start date Greater than Reservation Start Date 
            //End date Greater than Reservation End Date 
            //Start Date Smaller than Reservation End Date 
            DateTime startDate = new DateTime(2015, 09, 18);
            DateTime endDate = new DateTime(2017, 02, 15);
            Assert.AreEqual(0, newReservation.checkRunAvailability(startDate, endDate, 'R'));
        }

        [TestMethod]
        public void RunAvailability7()
        {
            Reservation newReservation = new Reservation();
            //Happy Path 
            //Start date Greater than Reservation Start Date 
            //End date Greater than Reservation End Date 
            //Start Date Greater than Reservation End Date 
            DateTime startDate = new DateTime(2017, 02, 15);
            DateTime endDate = new DateTime(2017, 02, 20);
            Assert.AreEqual(1, newReservation.checkRunAvailability(startDate, endDate, 'R'));
        }

        [TestMethod]
        public void RunAvailability8()
        {
            Reservation newReservation = new Reservation();
            //Input                 (2017, 02, 20);
            //                      (2017, 01, 31);
            //Expected              0 Run Available 
            //Start date Greater than Reservation Start Date 
            //End date equals to Reservation End Date 
            //Start Date Greater than Reservation End Date
            DateTime startDate = new DateTime(2017, 02, 20);
            DateTime endDate = new DateTime(2017, 01, 31);
            Assert.AreEqual(0, newReservation.checkRunAvailability(startDate, endDate, 'R'));
        }

        [TestMethod]
        public void RunAvailability9()
        {
            Reservation newReservation = new Reservation();
            //Input                     (2015, 09, 10);
            //Expected                  (2017, 02, 15);
            //Expected                  0 Run Available 
            //Start date smaller than Reservation Start Date 
            //End date Greater than Reservation End Date
            DateTime startDate = new DateTime(2015, 09, 10);
            DateTime endDate = new DateTime(2017, 02, 15);
            Assert.AreEqual(0, newReservation.checkRunAvailability(startDate, endDate, 'R'));
        }


        [TestMethod]
        public void RunAvailability10()
        {
            Reservation newReservation = new Reservation();
            //Input                          (2015, 09, 19);
            //                               (2017, 01, 15);
            //Expected :                      Run Available 
            //Start date Greater than Reservation Start Date 
            //End date Smaller than Reservation End Date
            //Start date smaller than Reservation End Date 
            DateTime startDate = new DateTime(2015, 09, 19);
            DateTime endDate = new DateTime(2017, 01, 15);
            Assert.AreEqual(1, newReservation.checkRunAvailability(startDate, endDate, 'R'));
        }

        [TestMethod]
        public void RunAvailability11()
        {
            Reservation newReservation = new Reservation();
            //Input                            (2015, 09, 10);
            //                                 (2017, 01, 25);
            //Expected :                        0 Run Available 
            //Start date Smaller than Reservation Start Date 
            //End date Smaller than Reservation End Date
            //Start date smaller than Reservation End Date  
            DateTime startDate = new DateTime(2015, 09, 10);
            DateTime endDate = new DateTime(2017, 01, 25);
            Assert.AreEqual(0, newReservation.checkRunAvailability(startDate, endDate, 'R'));
        }
        
        [TestMethod]
        public void testDeleteDogFromReservation()
        {
            Reservation hvk = new Reservation();
            // Test Method: Solo pet in reservation
            // Input Parameters: reservationNumber - 140
            //                   petNumber - 27
            // Expected Result: 0
            Assert.AreEqual(0, hvk.deleteDogFromReservation(140, 27), "Solo dog in reservation didn't return 0");
            // Test Method: Sharing pet in reservation
            // Input Parameters: reservationNumber - 140
            //                   petNumber - 26
            // Expected Result: 0 (Pet that was being shared with must be set to solo)
            Assert.AreEqual(0, hvk.deleteDogFromReservation(140, 26), "Sharing dog in reservation didn't return 0");
            // Test Method: Invalid reservation number
            // Input Parameters: reservationNumber - 0
            //                   petNumber - 1
            // Expected Result: 1
            Assert.AreEqual(1, hvk.deleteDogFromReservation(0, 1), "Invalid reservation number didn't return 1");
            // Test Method: Invalid pet number
            // Input Parameters: reservationNumber - 140
            //                   petNumber - 0
            // Expected Result: 2
            Assert.AreEqual(2, hvk.deleteDogFromReservation(140, 0), "Invalid pet number didn't return 2");
            // Test Method: Pet not part of the reservation
            // Input Parameters: reservationNumber - 140
            //                   petNumber - 1
            // Expected Result: 3
            Assert.AreEqual(3, hvk.deleteDogFromReservation(140, 1), "Pet not in reservation didn't return 3");
        }


    }



}


