﻿using Airline_ticket_sales_management.AControls;
using Airline_ticket_sales_management.DALs;
using Airline_ticket_sales_management.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline_ticket_sales_management.Service
{
    public class FlightService
    {
        private static FlightService _ins;
        public static FlightService Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new FlightService();
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task<(bool, int, string)> createFlight(FlightDTO flight)
        {
            (AirportDTO departureAirportFind, string label1) = await AirportDAL.Ins.findAirport(flight.DepartureAirportName);
            if (departureAirportFind == null)
                return (false, 0, label1);

            (AirportDTO arrivalAirportFind, string label2) = await AirportDAL.Ins.findAirport(flight.ArrivalAirportName);
            if (arrivalAirportFind == null)
                return (false, 1, label2);

            (PlaneDTO plane, string label3) = await PlaneDAL.Ins.findPlane(flight.PlaneID);
            if (plane == null)
                return (false, 2, label3);

            flight.DepartureAirportCode = departureAirportFind.AirportID;
            flight.ArrivalAirportCode = arrivalAirportFind.AirportID;

            (bool isCreate, string label, string newID) = await FlightDAL.Ins.createAirport(flight);

            if (isCreate)
            {
                return (true, -1, label);
            }
            else
            {
                return (false, -1, label);
            }
        }
    }
}