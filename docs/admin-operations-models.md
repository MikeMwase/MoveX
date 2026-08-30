# MoveX Admin Operations Domain

The first Admin Operations domain slice contains the core operational control entities:

- Admin users, roles and permissions
- Dispatch jobs and driver assignments
- Booking status history
- Live driver locations
- Operational incidents
- Customer support tickets
- Audit logging

## Design principles

1. Operational history is append-oriented. Assignment and booking status changes are retained rather than overwritten.
2. Dispatch is separate from booking so a booking can survive multiple assignment attempts.
3. Driver locations are time-series data and should be indexed by driver and recorded timestamp.
4. Administrative access uses roles and granular permissions.
5. Sensitive operational changes must be auditable.
6. Domain entities do not contain UI concerns.

## Next domain slice

The next implementation should add the core shared entities and relationships:

- CustomerProfile
- CustomerAddress
- DriverProfile
- Vehicle
- VehicleType
- DriverDocument
- Booking
- BookingItem
- Trip
- Payment
- PricingRule
- MovingService

Then EF Core configurations and the DbContext can establish the complete relational model.
