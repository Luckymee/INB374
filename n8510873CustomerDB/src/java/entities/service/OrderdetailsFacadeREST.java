/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package entities.service;

import entities.Orderdetails;
import entities.OrderdetailsPK;
import java.util.List;
import javax.ejb.Stateless;
import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;
import javax.ws.rs.Consumes;
import javax.ws.rs.DELETE;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.PUT;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.core.PathSegment;

/**
 *
 * @author Connor
 */
@Stateless
@Path("entities.orderdetails")
public class OrderdetailsFacadeREST extends AbstractFacade<Orderdetails> {
    @PersistenceContext(unitName = "n8510873CustomerDBPU")
    private EntityManager em;

    private OrderdetailsPK getPrimaryKey(PathSegment pathSegment) {
        /*
         * pathSemgent represents a URI path segment and any associated matrix parameters.
         * URI path part is supposed to be in form of 'somePath;productsproductCode=productsproductCodeValue;ordersorderNumber=ordersorderNumberValue'.
         * Here 'somePath' is a result of getPath() method invocation and
         * it is ignored in the following code.
         * Matrix parameters are used as field names to build a primary key instance.
         */
        entities.OrderdetailsPK key = new entities.OrderdetailsPK();
        javax.ws.rs.core.MultivaluedMap<String, String> map = pathSegment.getMatrixParameters();
        java.util.List<String> productsproductCode = map.get("productsproductCode");
        if (productsproductCode != null && !productsproductCode.isEmpty()) {
            key.setProductsproductCode(new java.lang.Integer(productsproductCode.get(0)));
        }
        java.util.List<String> ordersorderNumber = map.get("ordersorderNumber");
        if (ordersorderNumber != null && !ordersorderNumber.isEmpty()) {
            key.setOrdersorderNumber(new java.lang.Integer(ordersorderNumber.get(0)));
        }
        return key;
    }

    public OrderdetailsFacadeREST() {
        super(Orderdetails.class);
    }

    @POST
    @Override
    @Consumes({"application/xml", "application/json"})
    public void create(Orderdetails entity) {
        super.create(entity);
    }

    @PUT
    @Override
    @Consumes({"application/xml", "application/json"})
    public void edit(Orderdetails entity) {
        super.edit(entity);
    }

    @DELETE
    @Path("{id}")
    public void remove(@PathParam("id") PathSegment id) {
        entities.OrderdetailsPK key = getPrimaryKey(id);
        super.remove(super.find(key));
    }

    @GET
    @Path("{id}")
    @Produces({"application/xml", "application/json"})
    public Orderdetails find(@PathParam("id") PathSegment id) {
        entities.OrderdetailsPK key = getPrimaryKey(id);
        return super.find(key);
    }

    @GET
    @Override
    @Produces({"application/xml", "application/json"})
    public List<Orderdetails> findAll() {
        return super.findAll();
    }

    @GET
    @Path("{from}/{to}")
    @Produces({"application/xml", "application/json"})
    public List<Orderdetails> findRange(@PathParam("from") Integer from, @PathParam("to") Integer to) {
        return super.findRange(new int[]{from, to});
    }

    @GET
    @Path("count")
    @Produces("text/plain")
    public String countREST() {
        return String.valueOf(super.count());
    }

    @Override
    protected EntityManager getEntityManager() {
        return em;
    }
    
}
